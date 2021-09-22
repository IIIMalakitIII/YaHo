using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.CustomerReview;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview;
using YaHo.YaHoApiService.BLL.Domain.Validations;
using YaHo.YaHoApiService.Common.Exceptions;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class CustomerReviewService : ICustomerReviewService
    {
        private readonly ICustomerReviewDataService _customerReviewDataService;

        private readonly ICustomerDataService _customerDataService;

        private readonly IUserDataService _userDataService;

        private readonly IMapper _mapper;

        private readonly UserValidator _userValidator;

        private readonly CustomerValidator _customerValidator;

        public CustomerReviewService(ICustomerReviewDataService customerReviewDataService,
            ICustomerDataService customerDataService,
            IUserDataService userDataService, IMapper mapper)
        {
            _customerReviewDataService = customerReviewDataService;
            _userDataService = userDataService;
            _customerDataService = customerDataService;
            _mapper = mapper;
            _userValidator = new UserValidator(userDataService);
            _customerValidator = new CustomerValidator(customerDataService);
        }

        public async Task AddCustomerReview(CustomerReviewViewData model, int userCustomerId)
        {
            if (model.CustomerId == userCustomerId)
            {
                throw new BusinessLogicException("You can’t leave a review for yourself!");
            }

            await _customerValidator.CheckCustomerWithThisIdExists(model.CustomerId);
            await _userValidator.CheckUserWithThisIdExists(model.UserId);

            await _customerReviewDataService.AddCustomerReviewAsync(model);
            await UpdateCustomerRating(model.CustomerId, model.Mark);
        }

        public async Task<List<CustomerReviewViewData>> GetCustomerReviews(int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);

            var customerReviewsViewData = await _customerReviewDataService.GetCustomerReviewAsync(customerId);

            return customerReviewsViewData;
        }


        #region Private_method

        private async Task UpdateCustomerRating(int customerId, int newMark)
        {
            var customer = await _customerDataService.GetCustomerWithoutIncludeAsync(customerId);

            customer.Rating = await _customerReviewDataService.CalculateCustomerRating(customerId);
            customer.TotalReviewCount += 1;

            await _customerDataService.UpdateCustomerAsync(customer);
        }

        #endregion
    }
}

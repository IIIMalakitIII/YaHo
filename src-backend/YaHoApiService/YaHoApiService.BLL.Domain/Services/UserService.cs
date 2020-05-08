using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;
using YaHo.YaHoApiService.BLL.Domain.Validations;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDataService _userDataService;

        private readonly ICustomerDataService _customerDataService;

        private readonly IDeliveryDataService _deliveryDataService;

        private readonly IMapper _mapper;

        private readonly UserValidator _userValidator;

        public UserService(IUserDataService userDataService, ICustomerDataService customerDataService,
            IDeliveryDataService deliveryDataService, IMapper mapper)
        {
            _userDataService = userDataService;
            _mapper = mapper;
            _deliveryDataService = deliveryDataService;
            _customerDataService = customerDataService;
            _userValidator = new UserValidator(userDataService);
        }


        public async Task CreateUser(CreateUserViewData model)
        {
            await _userValidator.CheckUserEmailAlreadyExistsForCreate(model.Email);

            model.InitialDate = DateTime.UtcNow;
            model.UserName = model.Email;

            var createdId = await _userDataService.CreateUserAsync(model);
            var newDelivery = new DeliveryViewData() {UserId = createdId};
            var newCustomer = new CustomerViewData() { UserId = createdId };
            await _deliveryDataService.CreateDeliveryForNewUserAsync(newDelivery);
            await _customerDataService.CreateCustomerForNewUserAsync(newCustomer);

        }

        public async Task<UserViewData> GetUserById(string userId)
        {
            await _userValidator.CheckUserWithThisIdExists(userId);

            var user = await _userDataService.GetUserAsync(userId);

            return user;
        }

        public async Task ChangePassword(string userId, string currentPassword, string newPassword)
        {
            await _userValidator.CheckUserWithThisIdExists(userId);

            await _userDataService.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        public async Task<List<UserViewData>> GetAllUser()
        {
            var users = await _userDataService.GetAllUserAsync();

            return users;
        }

        public async Task UpdateUser(UpdateUserInfoViewData model)
        {
            await _userValidator.CheckUserWithThisIdExists(model.Id);

            await _userDataService.UpdateUserAsync(model);
        }

        public async Task<string> SignIn(string email, string password)
        {
            await _userValidator.CheckUserEmailExists(email);

            var token = await _userDataService.GenerateTokenForUser(email, password);

            return token;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.CustomerReview;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview;
using YaHo.YaHoApiService.Controllers;
using YaHo.YaHoApiService.ViewModels.CustomerReviewViewModels;

namespace YaHoApiService.Controllers
{
    [Route("api/CustomerReviews")]
    [ApiController]
    public class CustomerReviewController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewController(IMapper mapper, ICustomerReviewService customerReviewService)
        {
            _mapper = mapper;
            _customerReviewService = customerReviewService;
        }


        [HttpPost]
        public async Task<IActionResult> DeliveryReview(LeaveCustomerReview model)
        {
            var reviewViewData = _mapper.Map<CustomerReviewViewData>(model);
            reviewViewData.UserId = CurrentUser.UserId;

            await _customerReviewService.AddCustomerReview(reviewViewData, CurrentUser.CustomerId);

            return Ok();
        }

        [HttpGet("get-customer-reviews/{customerId}")]
        public async Task<ActionResult<List<CustomerReviewViewModel>>> DeliveryReview(int customerId)
        {
            var customerReviewsViewData = await _customerReviewService.GetCustomerReviews(customerId);

            var customerReviewsViewModel = _mapper.Map<List<CustomerReviewViewModel>>(customerReviewsViewData);

            return Ok(customerReviewsViewModel);
        }
    }
}
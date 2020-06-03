using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.DeliveryReview;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview;
using YaHo.YaHoApiService.Controllers;
using YaHo.YaHoApiService.ViewModels.DeliveryReviewViewModels;

namespace YaHoApiService.Controllers
{
    [Route("api/DeliveryReviews")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class DeliveryReviewController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IDeliveryReviewService _deliveryReviewService;

        public DeliveryReviewController(IMapper mapper, IDeliveryReviewService deliveryReviewService)
        {
            _mapper = mapper;
            _deliveryReviewService = deliveryReviewService;
        }


        [HttpPost]
        public async Task<IActionResult> DeliveryReview(LeaveDeliveryReview model)
        {
            var reviewViewData = _mapper.Map<DeliveryReviewViewData>(model);
            reviewViewData.UserId = CurrentUser.UserId;

            await _deliveryReviewService.AddDeliveryReview(reviewViewData, CurrentUser.DeliveryId);

            return Ok();
        }

        [HttpGet("get-delivery-reviews/{deliveryId}")]
        public async Task<ActionResult<List<DeliveryReviewViewModel>>> DeliveryReview(int deliveryId)
        {
            var deliveryReviewsViewData = await _deliveryReviewService.GetDeliveryReviews(deliveryId);

            var deliveryReviewsViewModel = _mapper.Map<List<DeliveryReviewViewModel>>(deliveryReviewsViewData);

            return Ok(deliveryReviewsViewModel);
        }
    }
}
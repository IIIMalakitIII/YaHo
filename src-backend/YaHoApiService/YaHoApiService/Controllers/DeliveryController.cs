using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.ViewModels.DeliveryViewModels;

namespace YaHo.YaHoApiService.Controllers
{
    [Route("api/Deliveries")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class DeliveryController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IMapper mapper, IDeliveryService deliveryService)
        {
            _mapper = mapper;
            _deliveryService = deliveryService;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDeliveryDescription(string description)
        {
            await _deliveryService.UpdateDeliveryDescription(CurrentUser.DeliveryId, description);

            return Ok();
        }

        [HttpGet("{deliveryId}")]
        public async Task<ActionResult<DeliveryViewModel>> Delivery(int deliveryId)
        {
            var deliveryViewData = await _deliveryService.GetDelivery(deliveryId);

            var deliveryViewModel = _mapper.Map<DeliveryViewModel>(deliveryViewData);

            return Ok(deliveryViewModel);
        }

        [HttpGet("my-delivery-info")]
        public async Task<ActionResult<DeliveryViewModel>> MyDeliveryInfo()
        {
            var deliveryViewData = await _deliveryService.GetDelivery(CurrentUser.DeliveryId);

            var deliveryViewModel = _mapper.Map<DeliveryViewModel>(deliveryViewData);

            return Ok(deliveryViewModel);
        }


        [HttpGet("delivery-info-by-user-id/{userId}")]
        public async Task<ActionResult<DeliveryViewModel>> CustomerInfoByUserId(string userId)
        {
            var deliveryViewData = await _deliveryService.GetDeliveryInfoByUserId(userId);

            var deliveryViewModel = _mapper.Map<DeliveryViewModel>(deliveryViewData);

            return Ok(deliveryViewModel);
        }

        [HttpGet("AllDeliveries")]
        public async Task<ActionResult<List<DeliveryViewModel>>> Delivery()
        {
            var deliveriesViewData = await _deliveryService.GetAllDelivery();

            var deliveriesViewModel = _mapper.Map<List<DeliveryViewModel>>(deliveriesViewData);

            return Ok(deliveriesViewModel);
        }
    }
}
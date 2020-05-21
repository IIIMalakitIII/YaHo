using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.ViewModels.DeliveryViewModels;
using YaHo.YaHoApiService.ViewModels.DeliveryViewModels.Update;

namespace YaHo.YaHoApiService.Controllers
{
    [Route("api/Deliveries")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class DeliveryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IMapper mapper, IDeliveryService deliveryService)
        {
            _mapper = mapper;
            _deliveryService = deliveryService;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDelivery(UpdateDeliveryInfoViewModel model)
        {
            await _deliveryService.UpdateDeliveryDescription(model.DeliveryId, model.Description);

            return Ok();
        }

        [HttpGet("{deliveryId}")]
        public async Task<ActionResult<DeliveryViewModel>> Delivery(int deliveryId)
        {
            var deliveryViewData = await _deliveryService.GetDelivery(deliveryId);

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
        public async Task<ActionResult<IEnumerable<DeliveryViewModel>>> Delivery()
        {
            var deliveriesViewData = await _deliveryService.GetAllDelivery();

            var deliveriesViewModel = _mapper.Map<IEnumerable<DeliveryViewModel>>(deliveriesViewData);

            return Ok(deliveriesViewModel);
        }
    }
}
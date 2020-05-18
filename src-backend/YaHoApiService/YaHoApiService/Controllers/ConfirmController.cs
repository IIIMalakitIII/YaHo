using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;
using YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm;
using YaHo.YaHoApiService.ViewModels.OrderViewModels;
using YaHoA.YaHoApiService.ViewModels.ConfirmViewModels;
using YaHoApiService.ViewModels.ConfirmViewModels.Update;

namespace YaHo.YaHoApiService.Controllers
{
    [Route("api/Confirms")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ConfirmController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IConfirmService _confirmService;

        public ConfirmController(IMapper mapper, IConfirmService confirmService)
        {
            _mapper = mapper;
            _confirmService = confirmService;
        }

        [HttpPost("confirm-change-delivery-charge")]
        public async Task<ActionResult> ConfirmChangeDeliveryCharge(CreateConfirmDeliveryChargeViewModel model)
        {
            var confirmViewData = _mapper.Map<CreateConfirmDeliveryChargeViewData>(model);

            await _confirmService.CreateConfirmChangeDeliveryCharge(confirmViewData, CurrentUser.UserId, CurrentUser.CustomerId);

            return Ok();
        }

        [HttpGet("confirms-delivery-charge/{orderId}")]
        public async Task<ActionResult<IEnumerable<ConfirmDeliveryChargeViewModel>>> ConfirmDeliveryCharge(int orderId)
        {
            var confirmsViewData = await _confirmService.GetConfirmsDeliveryCharge(orderId, CurrentUser.UserId);

            var confirmsViewModel = _mapper.Map<List<ConfirmDeliveryChargeViewModel>>(confirmsViewData);

            return Ok(confirmsViewModel);
        }

        [HttpPut("update-confirm-delivery-charge")]
        public async Task<ActionResult<IEnumerable<ConfirmDeliveryChargeViewModel>>> ConfirmDeliveryCharge(UpdateDeliveryChargeViewModel model)
        {
            var confirmsViewData = await _confirmService.UpdateConfirmDeliveryCharge(model.Id, model.DeliveryConfirm, CurrentUser.DeliveryId);

            var confirmsViewModel = _mapper.Map<List<ConfirmDeliveryChargeViewModel>>(confirmsViewData);

            return Ok(confirmsViewModel);
        }

        [HttpDelete("delete-confirm-delivery-charge/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> DeleteConfirmChangeDeliveryCharge(int orderId)
        {
            await _confirmService.DeleteConfirmChangeDeliveryCharge(orderId, CurrentUser.UserId, CurrentUser.CustomerId);

            return Ok();
        }
    }
}
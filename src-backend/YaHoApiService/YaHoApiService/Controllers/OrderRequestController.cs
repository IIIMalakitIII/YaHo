using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.OrderRequest;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest;
using YaHo.YaHoApiService.Controllers;
using YaHo.YaHoApiService.ViewModels.OrderRequestViewModels;
using YaHoApiService.TelegramBot;

namespace YaHoApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class OrderRequestController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IOrderRequestService _orderRequestService;
        private readonly IOrderService _orderService;

        public OrderRequestController(IMapper mapper, IOrderRequestService orderRequestService, IOrderService orderService)
        {
            _mapper = mapper;
            _orderRequestService = orderRequestService;
            _orderService = orderService;


        }

        [HttpPost("create-order-request")]
        public async Task<IActionResult> CreateOrderRequest(int orderId)
        {
            await _orderRequestService.CreateOrderRequest(orderId, CurrentUser.DeliveryId);
            var customerUser = await _orderService.GetUserByOrderId(orderId);
            Bot.SendNotification(customerUser.TelegramId);
            return Ok();
        }

        [HttpPut("approve-order-request")]
        public async Task<IActionResult> ApproveOrderRequest(int requestId)
        {
            await _orderRequestService.ApproveOrderRequest(requestId, CurrentUser.CustomerId);
            return Ok();
        }

        [HttpPut("reject-order-request")]
        public async Task<IActionResult> RejectOrderRequest(int requestId)
        {
            await _orderRequestService.RejectOrderRequest(requestId, CurrentUser.CustomerId);

            return Ok();
        }

        [HttpDelete("delete-my-order-request")]
        public async Task<IActionResult> DeleteOrderRequest(int requestId)
        {
            await _orderRequestService.DeleteOrderRequest(requestId, CurrentUser.DeliveryId);

            return Ok();
        }

        [HttpDelete("delete-order-request-like-customer")]
        public async Task<IActionResult> DeleteOrderRequestLikeCustomer(int requestId)
        {
            await _orderRequestService.DeleteOrderRequestLikeCustomer(requestId, CurrentUser.CustomerId);

            return Ok();
        }

        [HttpGet("get-my-request")]
        public async Task<ActionResult<IEnumerable<OrderRequestViewModel>>> GetMyRequests()
        {
            var orderRequestsViewData = await _orderRequestService.GetMyRequests(CurrentUser.DeliveryId);

            var orderRequestsViewModel = _mapper.Map<IEnumerable<OrderRequestViewModel>>(orderRequestsViewData);
            
            return Ok(orderRequestsViewModel);
        }

        [HttpGet("get-order-request/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderRequestViewModel>>> GetOrderRequests(int orderId)
        {
            var orderRequestsViewData = await _orderRequestService.GetOrderRequests(orderId, CurrentUser.CustomerId);

            var orderRequestsViewModel = _mapper.Map<IEnumerable<OrderRequestViewModel>>(orderRequestsViewData);

            return Ok(orderRequestsViewModel);
        }

        [HttpDelete("reject-all-order-request")]
        public async Task<IActionResult> RejectAllOrderRequest(int orderId)
        {
            await _orderRequestService.RejectAllOrderRequest(orderId, CurrentUser.CustomerId);

            return Ok();
        }
    }
}
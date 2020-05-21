using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order.Update;
using YaHo.YaHoApiService.BLL.Contracts.ServiceResults.CreateResult;
using YaHo.YaHoApiService.ViewModels.OrderViewModels;
using YaHo.YaHoApiService.ViewModels.OrderViewModels.Update;

namespace YaHo.YaHoApiService.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class OrderController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<CreatedViewData>> Order(CreateOrderViewModel model)
        {
            var userViewData = _mapper.Map<OrderViewData>(model);
            userViewData.CustomerId = CurrentUser.CustomerId;

            var created = await _orderService.CreateOrder(userViewData, CurrentUser.UserId);

            return Ok(created);
        }

        [HttpGet("order-list")]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> OrderByFilter([FromQuery] OrderFilterViewModel model)
        {
            var filterViewData = _mapper.Map<OrderFilterViewData>(model);

            var filteredOrder = await _orderService.GetOrdersByFilter(filterViewData);

            return Ok(filteredOrder);
        }

        [HttpGet("order-by-id/{orderId}")]
        public async Task<ActionResult<OrderViewModel>> Order(int orderId)
        {
            var orderViewData = await _orderService.GetOrderById(orderId, CurrentUser.CustomerId);

            var orderViewModel = _mapper.Map<OrderViewModel>(orderViewData);

            return Ok(orderViewModel);
        }

        [HttpGet("my-order-like-customer")]
        public async Task<ActionResult<OrderViewModel>> CustomerOrder()
        {
            var ordersViewData = await _orderService.GetCustomerOrders(CurrentUser.CustomerId);

            var ordersViewModel = _mapper.Map<List<OrderViewModel>>(ordersViewData);

            return Ok(ordersViewModel);
        }

        [HttpGet("my-order-like-delivery")]
        public async Task<ActionResult<OrderViewModel>> DeliveryOrder()
        {
            var ordersViewData = await _orderService.GetDeliveryOrders(CurrentUser.DeliveryId);

            var ordersViewModel = _mapper.Map<List<OrderViewModel>>(ordersViewData);

            return Ok(ordersViewModel);
        }

        [HttpPut("update-order-info")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderViewModel model)
        {
            var orderViewData = _mapper.Map<UpdateOrderViewData>(model);
            await _orderService.UpdateOrderInfo(orderViewData, CurrentUser.CustomerId);

            return Ok();
        }

    }
}
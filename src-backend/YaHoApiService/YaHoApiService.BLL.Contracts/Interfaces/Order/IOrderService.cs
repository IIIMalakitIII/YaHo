﻿using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order.Update;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;
using YaHo.YaHoApiService.BLL.Contracts.ServiceResults.CreateResult;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order
{
    public interface IOrderService
    {
        Task<List<OrderViewData>> GetOrdersByFilter(OrderFilterViewData filter);

        Task<OrderViewData> GetOrderById(int orderId, int customerId);

        Task<List<OrderViewData>> GetDeliveryOrders(int deliveryId);

        Task<List<OrderViewData>> GetCustomerOrders(int customerId);

        Task<CreatedViewData> CreateOrder(OrderViewData model, string userId);

        Task<UserViewData> GetUserByOrderId(int orderId);

        Task UpdateOrderInfo(UpdateOrderViewData model, int customerId);
    }
}

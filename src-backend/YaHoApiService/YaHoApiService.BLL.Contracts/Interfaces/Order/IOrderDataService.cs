using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order.Update;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order
{
    public interface IOrderDataService
    {
        Task<bool> IsOrderWithIdExistsAsync(int id);

        Task<bool> IsOrderWithIdInExpectationAsync(int orderId);

        Task<bool> IsOrderWithIdInProcessAsync(int orderId);

        Task<bool> IsOrderWithIdInCreatingAsync(int orderId);

        Task<bool> ThisUserHaveAccessAsync(int orderId, string userId);

        Task<bool> OrderOfThisCustomerAsync(int orderId, int customerId);

        Task<int> CreateOrderAsync(OrderViewData model);

        Task<List<OrderViewData>> GetOrdersByFilter(OrderFilterViewData filter);

        Task<OrderViewData> GetOrderByIdAsync(int orderId);

        Task<OrderViewData> GetOrderByIdForCustomerAsync(int orderId);

        Task<List<OrderViewData>> GetOrdersForCustomerAsync(int customerId);

        Task<List<OrderViewData>> GetOrdersForDeliveryAsync(int deliveryId);

        Task DeleteOrderAfterFailureAsync(int orderId);

        Task UpdateOrderAsync(OrderViewData order);

        Task UpdateOrderInfoAsync(UpdateOrderViewData order);

        Task<OrderViewData> GetOrderByIdWithoutIncludeAsync(int orderId);
    }
}

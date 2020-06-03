using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.OrderRequest
{
    public interface IOrderRequestService
    {
        Task CreateOrderRequest(int orderId, int deliveryId);

        Task ApproveOrderRequest(int requestId, int customerId);

        Task RejectOrderRequest(int requestId, int customerId);

        Task DeleteOrderRequest(int requestId, int deliveryId);

        Task DeleteOrderRequestLikeCustomer(int requestId, int customerId);

        Task<List<OrderRequestViewData>> GetMyRequests(int deliveryId);

        Task<List<OrderRequestViewData>> GetOrderRequests(int orderId, int customerId);

        Task RejectAllOrderRequest(int orderId, int customerId);

    }
}

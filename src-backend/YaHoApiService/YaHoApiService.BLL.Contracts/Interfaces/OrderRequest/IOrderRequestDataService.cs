using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.OrderRequest
{
    public interface IOrderRequestDataService
    {
        Task CreateOrderRequestAsync(OrderRequestViewData model);

        Task<bool> IsOrderRequestWithIdExistsAsync(int requestId);

        Task<bool> SomeRequestHasAlreadyBeenApproved(int orderId);

        Task<bool> ThisDeliveryHaveAccessFoRThisOrderRequest(int requestId, int deliveryId);

        Task<List<OrderRequestViewData>> GetOrderRequestsAsync(int orderId);

        Task<List<OrderRequestViewData>> GetMyOrderRequestsAsync(int deliveryId);

        Task<OrderRequestViewData> GetOrderRequestByIdWithoutIncludeAsync(int requestId);

        Task ApproveOrderRequestAsync(int requestId);

        Task RejectOrderRequestAsync(int requestId);

        Task DeleteOrderRequestAsync(int requestId);

        Task RejectAllOrderRequestsAsync(int orderId);

        Task RejectApprovedDeliveryToOrderAsync(int orderId);
    }
}

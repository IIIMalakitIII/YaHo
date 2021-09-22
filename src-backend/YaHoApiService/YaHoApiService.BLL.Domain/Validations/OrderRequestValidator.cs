using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.OrderRequest;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;

namespace YaHo.YaHoApiService.BLL.Domain.Validations
{
    public class OrderRequestValidator
    {
        private readonly IOrderRequestDataService _orderRequestDataService;

        public OrderRequestValidator(IOrderRequestDataService orderRequestDataService)
        {
            _orderRequestDataService = orderRequestDataService;
        }
        public async Task CheckOrderRequestWithThisIdExists(int requestId)
        {
            if (!await _orderRequestDataService.IsOrderRequestWithIdExistsAsync(requestId))
            {
                throw new ValidationException($"No order request with this id: '{requestId}'.");
            }
        }

        public async Task CheckThisDeliveryHaveAccessFoRThisOrderRequest(int requestId, int deliveryId)
        {
            if (!await _orderRequestDataService.ThisDeliveryHaveAccessFoRThisOrderRequest(requestId, deliveryId))
            {
                throw new ValidationException($"You do not have access to this request.");
            }
        } 

        public async Task CheckSomeRequestHasAlreadyBeenApproved(int orderId)
        {
            if (await _orderRequestDataService.SomeRequestHasAlreadyBeenApproved(orderId))
            {
                throw new ValidationException("Someone already approved.");
            }
        }
    }
}

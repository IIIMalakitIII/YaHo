using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;

namespace YaHo.YaHoApiService.BLL.Domain.Validations
{
    internal class ConfirmValidator
    {
        private readonly IConfirmDataService _confirmDataService;

        public ConfirmValidator(IConfirmDataService confirmDataService)
        {
            _confirmDataService = confirmDataService;
        }

        public async Task AnyDeliveryChargeActiveConfirm(int orderId)
        {
            if (await _confirmDataService.AnyDeliveryChargeActiveConfirmAsync(orderId))
            {
                throw new ValidationException($"You cannot make a new confirmation while another is active.");
            }
        }

        public async Task CheckConfirmDeliveryChargeExists(int confirmId)
        {
            if (!await _confirmDataService.CheckConfirmDeliveryChargeExistsAsync(confirmId))
            {
                throw new ValidationException($"There is no such confirmation with this id {confirmId}.");
            }
        }

        public async Task CheckConfirmDeliveryChargeOfThisCustomer(int confirmId,int customerId)
        {
            if (!await _confirmDataService.CheckConfirmDeliveryChargeOfThisCustomerAsync(confirmId, customerId))
            {
                throw new ValidationException($"You cannot change this confirmation.");
            }
        }

        public async Task CheckThisDeliveryHaveAccessToDeliveryCharge(int confirmId, int deliveryId)
        {
            if (!await _confirmDataService.CheckThisDeliveryHaveAccessToDeliveryChargeAsync(confirmId, deliveryId))
            {
                throw new ValidationException($"You don't have access to this action.");
            }
        }

        public async Task CheckConfirmDeliveryChargeNotAnswered(int confirmId)
        {
            if (!await _confirmDataService.CheckConfirmDeliveryChargeNotAnsweredAsync(confirmId))
            {
                throw new ValidationException($"You can't update this confirmation.");
            }
        }
    }
}

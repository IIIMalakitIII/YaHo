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

        #region ConfirmDeliveryCharge

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

        public async Task CheckConfirmDeliveryChargeOfThisCustomer(int confirmId, int customerId)
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

        #endregion

        #region ConfirmExpectedDate

        public async Task AnyExpectedDateActiveConfirm(int orderId)
        {
            if (await _confirmDataService.AnyExpectedDateActiveConfirmAsync(orderId))
            {
                throw new ValidationException($"You cannot make a new confirmation while another is active.");
            }
        }

        public async Task CheckConfirmExpectedDateExists(int confirmId)
        {
            if (!await _confirmDataService.CheckConfirmExpectedDateExistsAsync(confirmId))
            {
                throw new ValidationException($"There is no such confirmation with this id {confirmId}.");
            }
        }

        public async Task CheckThisCustomerHaveAccessToExpectedDate(int confirmId, int customerId)
        {
            if (!await _confirmDataService.CheckThisCustomerHaveAccessToExpectedDateAsync(confirmId, customerId))
            {
                throw new ValidationException($"You cannot change this confirmation.");
            }
        }

        public async Task CheckThisDeliveryHaveAccessToExpectedDate(int confirmId, int deliveryId)
        {
            if (!await _confirmDataService.CheckThisDeliveryHaveAccessToExpectedDateAsync(confirmId, deliveryId))
            {
                throw new ValidationException($"You don't have access to this action.");
            }
        }

        public async Task CheckConfirmExpectedDateNotAnswered(int confirmId)
        {
            if (!await _confirmDataService.CheckConfirmExpectedDateNotAnsweredAsync(confirmId))
            {
                throw new ValidationException($"You can't update this confirmation.");
            }
        }

        public async Task CheckThisUserHaveAccessToDelete(int id, string userId)
        {
            if (!await _confirmDataService.CheckThisUserHaveAccessToDeleteAsync(id, userId))
            {
                throw new ValidationException($"You don't have access to this confirmation.");
            }
        }


        #endregion

    }
}

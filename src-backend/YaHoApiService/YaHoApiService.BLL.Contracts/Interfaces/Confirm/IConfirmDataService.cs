using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;

namespace YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm
{
    public interface IConfirmDataService
    {
        #region ConfirmDeliveryCharge

        Task<bool> CheckThisDeliveryHaveAccessToDeliveryChargeAsync(int id, int deliveryId);

        Task<bool> CheckConfirmDeliveryChargeNotAnsweredAsync(int id);

        Task<bool> AnyDeliveryChargeActiveConfirmAsync(int orderId);

        Task<bool> CheckConfirmDeliveryChargeExistsAsync(int id);

        Task<bool> CheckConfirmDeliveryChargeOfThisCustomerAsync(int id, int customerId);

        Task<bool> DeleteConfirmDeliveryChargeAsync(int confirmId);

        Task CreateConfirmForDeliveryChargeAsync(CreateConfirmDeliveryChargeViewData model);

        Task<ConfirmDeliveryChargeViewData> GetConfirmDeliveryChargeByIdAsync(int id);

        Task<List<ConfirmDeliveryChargeViewData>> GetConfirmsDeliveryChargeForOrderAsync(int orderId);

        Task UpdateConfirmDeliveryChargeAsync(int id, bool deliveryConfirm);


        #endregion

        #region ConfirmExpectedDate

        Task<bool> AnyExpectedDateActiveConfirmAsync(int orderId);

        Task<bool> CheckConfirmExpectedDateExistsAsync(int id);

        Task<bool> CheckThisCustomerHaveAccessToExpectedDateAsync(int id, int customerId);

        Task<bool> CheckThisDeliveryHaveAccessToExpectedDateAsync(int id, int deliveryId);

        Task<bool> CheckConfirmExpectedDateNotAnsweredAsync(int id);

        Task<bool> CheckThisUserHaveAccessToDeleteAsync(int id, string userId);

        Task CreateConfirmForExpectedDateAsync(CreateConfirmExpectedDateViewData model);

        Task<ConfirmExpectedDateViewData> GetConfirmExpectedDateByIdAsync(int id);

        Task<List<ConfirmExpectedDateViewData>> GetConfirmsExpectedDateForOrderAsync(int orderId);

        Task UpdateConfirmExpectedDateDeliveryAsync(int id, bool deliveryConfirm);

        Task UpdateConfirmExpectedDateCustomerAsync(int id, bool customerConfirm);

        Task<bool> DeleteConfirmExpectedDateAsync(int confirmId);

        #endregion
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;

namespace YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm
{
    public interface IConfirmService
    {
        #region ConfirmDeliveryCharge

        Task CreateConfirmChangeDeliveryCharge(CreateConfirmDeliveryChargeViewData model, string userId,
            int customerId);

        Task DeleteConfirmChangeDeliveryCharge(int id, string userId,
            int customerId);

        Task<List<ConfirmDeliveryChargeViewData>> GetConfirmsDeliveryCharge(int orderId, string userId);

        Task UpdateConfirmDeliveryCharge(int id, int deliveryId, string userId, bool deliveryConfirm);

        #endregion


        #region ConfirmExpectedDate

        Task CreateConfirmConfirmExpectedDateLikeCustomer(CreateConfirmExpectedDateViewData model, int customerId, string userId);

        Task CreateConfirmConfirmExpectedDateLikeDelivery(CreateConfirmExpectedDateViewData model, string userId);

        Task DeleteConfirmChangeExpectedDate(int id, string userId);

        Task<List<ConfirmExpectedDateViewData>> GetConfirmsExpectedDate(int orderId, string userId);

        Task UpdateConfirmExpectedDateLikeDelivery(int id, int deliveryId, bool deliveryConfirm);

        Task UpdateConfirmExpectedDateLikeCustomer(int id, int customerId, bool customerConfirm);

        #endregion
    }
}

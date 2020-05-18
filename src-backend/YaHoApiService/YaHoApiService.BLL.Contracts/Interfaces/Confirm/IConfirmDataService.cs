using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;

namespace YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm
{
    public interface IConfirmDataService
    {
        Task<bool> AnyDeliveryChargeActiveConfirmAsync(int orderId);

        Task CreateConfirmForDeliveryChargeAsync(CreateConfirmDeliveryChargeViewData model);

        Task<bool> CheckConfirmDeliveryChargeExistsAsync(int id);

        Task<bool> CheckConfirmDeliveryChargeOfThisCustomerAsync(int id, int customerId);

        Task<ConfirmDeliveryChargeViewData> GetConfirmDeliveryChargeByIdAsync(int id);

        Task<bool> DeleteConfirmDeliveryChargeAsync(int confirmId);

        Task<List<ConfirmDeliveryChargeViewData>> GetConfirmsDeliveryChargeForOrderAsync(int orderId);

        Task<bool> CheckThisDeliveryHaveAccessToDeliveryChargeAsync(int id, int deliveryId);

        Task<bool> CheckConfirmDeliveryChargeNotAnsweredAsync(int id);

        Task UpdateConfirmDeliveryChargeAsync(int id, bool deliveryConfirm);
    }
}

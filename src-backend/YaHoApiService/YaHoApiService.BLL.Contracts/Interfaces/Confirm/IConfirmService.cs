using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;

namespace YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm
{
    public interface IConfirmService
    {
        Task CreateConfirmChangeDeliveryCharge(CreateConfirmDeliveryChargeViewData model, string userId,
            int customerId);

        Task DeleteConfirmChangeDeliveryCharge(int id, string userId,
            int customerId);

        Task<List<ConfirmDeliveryChargeViewData>> GetConfirmsDeliveryCharge(int orderId, string userId);
        
    }
}

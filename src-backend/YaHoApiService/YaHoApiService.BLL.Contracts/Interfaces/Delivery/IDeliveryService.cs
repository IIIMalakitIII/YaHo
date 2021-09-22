using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery
{
    public interface IDeliveryService
    {
        Task UpdateDeliveryDescription(int deliveryId, string description);

        Task<DeliveryViewData> GetDelivery(int deliveryId);

        Task<List<DeliveryViewData>> GetAllDelivery();

        Task<DeliveryViewData> GetDeliveryInfoByUserId(string userId);
    }
}

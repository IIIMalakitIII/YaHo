using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery
{
    public interface IDeliveryDataService
    {
        Task CreateDeliveryForNewUserAsync(DeliveryViewData model);

        Task<bool> IsDeliveryWithIdExistsAsync(int id);

        Task<DeliveryViewData> GetDeliveryAsync(int id);

        Task UpdateDeliveryAsync(DeliveryViewData model);

        Task<List<DeliveryViewData>> GetAllDeliveryAsync();

        Task<DeliveryViewData> GetCustomerByUserIdAsync(string id);
    }
}

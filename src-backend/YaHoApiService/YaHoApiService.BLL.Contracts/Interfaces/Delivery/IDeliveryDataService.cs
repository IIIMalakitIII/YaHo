using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery
{
    public interface IDeliveryDataService
    {
        Task CreateDeliveryForNewUserAsync(DeliveryViewData model);
    }
}

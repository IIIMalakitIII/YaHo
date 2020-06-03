using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.LiqPayOrder;

namespace YaHo.YaHoApiService.BLL.Contracts.Interfaces.LiqPay
{
    public interface ILiqPayDataService
    {
        Task<string> CreateLiqPayOrderAsync(decimal money, string userId);

        Task<bool> IsLiqPayOrderWithIdExistsAsync(string id);

        Task<LiqPayOrderViewData> GetLiqPayOrderAsync(string id);
    }
}

using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.LiqPayOrder;

namespace YaHo.YaHoApiService.BLL.Contracts.Interfaces.LiqPay
{
    public interface ILiqPayService
    {
        Task LiqPayResult(string liqPayOrderId, decimal money);

        Task<LiqPayDataViewData> CreateLiqPayOrder(decimal money, string userId);
    }
}

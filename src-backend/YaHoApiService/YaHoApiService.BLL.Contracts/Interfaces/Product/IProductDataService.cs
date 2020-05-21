using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product
{
    public interface IProductDataService
    {
        Task CreateProductAsync(ProductViewData product);
    }
}

using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product
{
    public interface IProductService
    {
        Task CreateProduct(ProductViewData product, int customerId, string userId);
    }

}

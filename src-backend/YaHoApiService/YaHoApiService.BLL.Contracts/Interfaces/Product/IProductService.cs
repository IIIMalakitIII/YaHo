using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product.Update;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product
{
    public interface IProductService
    {
        Task CreateProduct(ProductViewData product, int customerId, string userId);

        Task<List<ProductViewData>> GetProductsByOrderId(int orderId, string userId);

        Task<ProductViewData> GetProductById(int productId, string userId);

        Task UpdateProductInfo(UpdateProductViewData model, int customerId);

        Task DeleteProduct(int productId, int customerId, string userId);
    }

}

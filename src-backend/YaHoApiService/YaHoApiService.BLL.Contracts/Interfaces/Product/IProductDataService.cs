using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product.Update;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product
{
    public interface IProductDataService
    {
        Task<bool> IsProductWithIdExistsAsync(int productId);

        Task<bool> CheckCustomerHaveAccess(int productId, int customerId);

        Task CreateProductAsync(ProductViewData product);

        Task<List<ProductViewData>> GetProductsByOrderIdAsync(int orderId);

        Task<ProductViewData> GetProductByIdAsync(int productId);

        Task UpdateProductInfoAsync(UpdateProductViewData model);

        Task UpdateProductPriceAsync(int projectId, int newPrice, int newTax);

        Task DeleteProductAsync(int projectId);
    }
}

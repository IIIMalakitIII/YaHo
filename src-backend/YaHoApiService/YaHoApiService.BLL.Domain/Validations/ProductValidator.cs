using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;

namespace YaHo.YaHoApiService.BLL.Domain.Validations
{
    internal class ProductValidator
    {
        private readonly IProductDataService _productDataService;

        public ProductValidator(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }
    }
}

using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;

namespace YaHo.YaHoApiService.BLL.Domain.Validations
{
    internal class ProductValidator
    {
        private readonly IProductDataService _productDataService;

        public ProductValidator(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public async Task CheckProductWithThisIdExists(int productId)
        {
            if (!await _productDataService.IsProductWithIdExistsAsync(productId))
            {
                throw new ValidationException($"No product with this id: '{productId}'.");
            }
        }

        public async Task CheckCustomerWithThisIdHaveAccess(int productId, int customerId)
        {
            if (!await _productDataService.CheckCustomerHaveAccess(productId, customerId))
            {
                throw new ValidationException($"Sorry but you can't change this product info.");
            }
        }
    }
}

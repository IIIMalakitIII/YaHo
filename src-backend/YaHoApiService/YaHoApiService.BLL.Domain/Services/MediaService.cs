using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Media;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.BLL.Domain.Validations;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaDataService _mediaDataService;

        private readonly IProductDataService _productDataService;

        private readonly ProductValidator _productValidator;

        private readonly CustomerValidator _customerValidator;

        private readonly OrderValidator _orderValidator;


        public MediaService(IOrderDataService orderDataService, IProductDataService productDataService,
            IMediaDataService mediaDataService, ICustomerDataService customerDataService)
        {
            _productDataService = productDataService;
            _mediaDataService = mediaDataService;
            _productValidator = new ProductValidator(productDataService);
            _customerValidator = new CustomerValidator(customerDataService);
            _orderValidator = new OrderValidator(orderDataService);
        }

        public async Task AddMediaToProduct(List<MediaViewData> model, int productId , int customerId)
        {
            await _productValidator.CheckProductWithThisIdExists(productId);
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _productValidator.CheckCustomerWithThisIdHaveAccess(productId, customerId);

            var product = await _productDataService.GetProductByIdAsync(productId);
            await _orderValidator.CheckOrderStatusInCreating(product.OrderId);

            await _mediaDataService.AddMediaToProductAsync(model);
        }

        public async Task DeleteMedia(int mediaId, int productId, int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _productValidator.CheckCustomerWithThisIdHaveAccess(productId, customerId);

            var product = await _productDataService.GetProductByIdAsync(productId);
            await _orderValidator.CheckOrderStatusInCreating(product.OrderId);

            await _mediaDataService.DeleteMediaAsync(mediaId);
        }
    }
}

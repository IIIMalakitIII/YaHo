using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product.Update;
using YaHo.YaHoApiService.BLL.Domain.Validations;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDataService _productDataService;

        private readonly IOrderDataService _orderDataService;

        private readonly IUserDataService _userDataService;

        private readonly IMapper _mapper;

        private readonly ProductValidator _productValidator;

        private readonly CustomerValidator _customerValidator;

        private readonly OrderValidator _orderValidator;

        private readonly UserValidator _userValidator;

        public ProductService(IOrderDataService orderDataService, IProductDataService productDataService,
            IUserDataService userDataService,
            IMapper mapper, ICustomerDataService customerDataService)
        {
            _orderDataService = orderDataService;
            _productDataService = productDataService;
            _userDataService = userDataService;
            _mapper = mapper;
            _userValidator = new UserValidator(userDataService); 
            _productValidator = new ProductValidator(productDataService);
            _customerValidator = new CustomerValidator(customerDataService);
            _orderValidator = new OrderValidator(orderDataService);
        }


        public async Task CreateProduct(ProductViewData product, int customerId, string userId)
        {
            await _orderValidator.CheckOrderWithThisIdExists(product.OrderId);
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderValidator.CheckOrderOfThisCustomer(product.OrderId, customerId);
            await _orderValidator.CheckOrderStatusInCreating(product.OrderId);
            await _userValidator.CheckUserHasEnoughMoney(userId, product.Price + product.Tax);

            await _productDataService.CreateProductAsync(product);
            await _userDataService.FreezeMoneyAsync(userId, product.Price + product.Tax);
        }

        public async Task<List<ProductViewData>> GetProductsByOrderId(int orderId, string userId)
        {
            await _orderValidator.CheckOrderWithThisIdExists(orderId);

            var orderInExpectation = await _orderDataService.IsOrderWithIdInExpectationAsync(orderId);
            var userHaveAccess = await _orderDataService.ThisUserHaveAccessAsync(orderId, userId);

            if (!orderInExpectation && !userHaveAccess)
            {
                return null;
            }

            var productsViewData = await _productDataService.GetProductsByOrderIdAsync(orderId);

            return productsViewData;
        }

        public async Task<ProductViewData> GetProductById(int productId, string userId)
        {
            await _productValidator.CheckProductWithThisIdExists(productId);

            var productViewData = await _productDataService.GetProductByIdAsync(productId);

            var orderInExpectation = await _orderDataService.IsOrderWithIdInExpectationAsync(productViewData.OrderId);
            var userHaveAccess = await _orderDataService.ThisUserHaveAccessAsync(productViewData.OrderId, userId);

            if (!orderInExpectation && !userHaveAccess)
            {
                return null;
            }

            return productViewData;
        }

        public async Task UpdateProductInfo(UpdateProductViewData model, int customerId)
        {
            await _productValidator.CheckProductWithThisIdExists(model.ProductId);
            await _productValidator.CheckCustomerWithThisIdHaveAccess(model.ProductId, customerId);

            await _productDataService.UpdateProductInfoAsync(model);

        }
    }
}

using System.Threading.Tasks;
using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.BLL.Domain.Validations;
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
    }
}

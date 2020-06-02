using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order.Update;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;
using YaHo.YaHoApiService.BLL.Contracts.ServiceResults.CreateResult;
using YaHo.YaHoApiService.BLL.Domain.Validations;
using YaHo.YaHoApiService.Common.Exceptions;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDataService _orderDataService;

        private readonly IUserDataService _userDataService;

        private readonly IMapper _mapper;

        private readonly DeliveryValidator _deliveryValidator;

        private readonly UserValidator _userValidator;

        private readonly CustomerValidator _customerValidator;

        private readonly OrderValidator _orderValidator;

        public OrderService(IOrderDataService orderDataService,
            IUserDataService userDataService, IMapper mapper, 
            IDeliveryDataService deliveryDataService,
            ICustomerDataService customerDataService)
        {
            _orderDataService = orderDataService;
            _mapper = mapper;
            _userDataService = userDataService;
            _userValidator = new UserValidator(userDataService);
            _deliveryValidator = new DeliveryValidator(deliveryDataService);
            _customerValidator = new CustomerValidator(customerDataService);
            _orderValidator = new OrderValidator(orderDataService);
        }

        // Complete
        public async Task<CreatedViewData> CreateOrder(OrderViewData model, string userId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(model.CustomerId);
            await _userValidator.CheckUserWithThisIdExists(userId);
            await _userValidator.CheckUserHasEnoughMoney(userId, model.DeliveryCharge);

            model.InitialDate = DateTime.UtcNow;
            model.OrderStatus = OrderStatus.Creating;

            var result = await _userDataService.FreezeMoneyAsync(userId, model.DeliveryCharge);
            if (!result)
            {
                throw new BusinessLogicException("There were problems during the debit from the account, the order was not created.");
            }

            var createdId = await _orderDataService.CreateOrderAsync(model);
            
            return new CreatedViewData(createdId);
        }

        public async Task<List<OrderViewData>> GetOrdersByFilter(OrderFilterViewData filter)
        {
            var filteredOrders = await _orderDataService.GetOrdersByFilter(filter);

            return filteredOrders;
        }
        public async Task<UserViewData> GetUserByOrderId(int orderId)
        {
            var order = await _orderDataService.GetAnyOrderByIdAsync(orderId);
            return order?.Customer?.User;
        }
        public async Task<OrderViewData> GetOrderById(int orderId, int customerId)
        {
            await _orderValidator.CheckOrderWithThisIdExists(orderId);
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);

            var isCustomerOrder = await _orderDataService.OrderOfThisCustomerAsync(orderId, customerId);

            if (isCustomerOrder)
            {
                var order = await _orderDataService.GetOrderByIdForCustomerAsync(orderId);

                return order;
            }
            else
            {
                var order = await _orderDataService.GetOrderByIdAsync(orderId);

                return order;
            }
        }

        public async Task<List<OrderViewData>> GetCustomerOrders(int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);

            var orders = await _orderDataService.GetOrdersForCustomerAsync(customerId);

            return orders;
        }

        public async Task<List<OrderViewData>> GetDeliveryOrders(int deliveryId)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);

            var orders = await _orderDataService.GetOrdersForDeliveryAsync(deliveryId);

            return orders;
        }

        //TODO
        public async Task UpdateOrderStatus(OrderUpdateStatusViewData model, int customerId)
        {
            await _orderValidator.CheckOrderWithThisIdExists(model.OrderId);
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderValidator.CheckOrderOfThisCustomer(model.OrderId, customerId);

            // Check order not done 

        }

        public async Task UpdateOrderInfo(UpdateOrderViewData model, int customerId)
        {
            await _orderValidator.CheckOrderWithThisIdExists(model.OrderId);
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderValidator.CheckOrderOfThisCustomer(model.OrderId, customerId);

            await _orderDataService.UpdateOrderInfoAsync(model);

        }

        public async Task UpdateOrderStatus(OrderStatus status, int orderId, int customerId)
        {
            await _orderValidator.CheckOrderWithThisIdExists(orderId);
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderValidator.CheckOrderOfThisCustomer(orderId, customerId);
            await _orderValidator.CheckOrderStatusNotInProcess(orderId);
            await _orderValidator.CheckOrderStatusNotDone(orderId);

            await _orderDataService.UpdateOrderStatusAsync(orderId, status);
        }

        public async Task DeleteOrder(int orderId, int customerId, string userId)
        {
            await _orderValidator.CheckOrderWithThisIdExists(orderId);
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderValidator.CheckOrderOfThisCustomer(orderId, customerId);
            await _orderValidator.CheckOrderStatusNotInProcess(orderId);
            await _orderValidator.CheckOrderStatusNotDone(orderId);

            var orderMoney = await _orderDataService.GetOrderMoneyAsync(orderId);

            await _userDataService.DefrostMoneyAsync(userId, orderMoney);
            await _orderDataService.DeleteOrderAsync(orderId);
        }

        #region Private_methods 
        private void UpdateGradeForUserResult(OrderUpdateStatusViewData model, int customerId)
        {
            switch (model.OrderStatus)
            {
                case OrderStatus.Canceled:
                {

                    break;
                }

                case OrderStatus.Done:
                {
                    break;
                }

                case OrderStatus.InProcess:
                {
                    break;
                }

                case OrderStatus.InExpectation:
                {
                    break;
                }

                case OrderStatus.Creating:
                {
                    break;
                }
            }
        }

        #endregion

    }
}

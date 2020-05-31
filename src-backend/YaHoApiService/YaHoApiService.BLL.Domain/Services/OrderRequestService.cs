using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.OrderRequest;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest;
using YaHo.YaHoApiService.BLL.Domain.Validations;
using YaHo.YaHoApiService.Common.Exceptions;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class OrderRequestService : IOrderRequestService
    {
        private readonly IOrderRequestDataService _orderRequestDataService;

        private readonly IOrderDataService _orderDataService;

        private readonly CustomerValidator _customerValidator;

        private readonly DeliveryValidator _deliveryValidator;

        private readonly OrderValidator _orderValidator;

        private readonly OrderRequestValidator _orderRequestValidator;

        public OrderRequestService(IOrderRequestDataService orderRequestDataService,
            IOrderDataService orderDataService, IDeliveryDataService deliveryDataService,
            ICustomerDataService customerDataService)
        {
            _orderRequestDataService = orderRequestDataService;
            _orderDataService = orderDataService;
            _deliveryValidator = new DeliveryValidator(deliveryDataService);
            _customerValidator = new CustomerValidator(customerDataService);
            _orderRequestValidator = new OrderRequestValidator(orderRequestDataService);
            _orderValidator = new OrderValidator(orderDataService);
        }

        public async Task CreateOrderRequest(int orderId, int deliveryId)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);
            await _orderValidator.CheckOrderWithThisIdExists(orderId);
            await _orderValidator.CheckOrderStatusInExpectation(orderId);
            await _orderRequestValidator.CheckSomeRequestHasAlreadyBeenApproved(orderId);

            var orderRequest = new OrderRequestViewData()
            {
                OrderId = orderId,
                DeliveryId = deliveryId,
                InitialDate = DateTime.UtcNow
            };

            await _orderRequestDataService.CreateOrderRequestAsync(orderRequest);
        }

        public async Task ApproveOrderRequest(int requestId, int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderRequestValidator.CheckOrderRequestWithThisIdExists(requestId);

            var orderRequest = await _orderRequestDataService.GetOrderRequestByIdWithoutIncludeAsync(requestId);
            
            await _orderValidator.CheckOrderWithThisIdExists(orderRequest.OrderId);
            await _orderValidator.CheckOrderOfThisCustomer(orderRequest.OrderId, customerId);
            await _orderValidator.CheckOrderStatusInExpectation(orderRequest.OrderId);
            await _orderValidator.CheckThisOrderNoOneApproved(orderRequest.OrderId);

            await _orderRequestDataService.ApproveOrderRequestAsync(requestId);
            await _orderDataService.UpdateOrderStatusAsync(orderRequest.OrderId, OrderStatus.InProcess);
        }

        public async Task RejectOrderRequest(int requestId, int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderRequestValidator.CheckOrderRequestWithThisIdExists(requestId);

            var orderRequest = await _orderRequestDataService.GetOrderRequestByIdWithoutIncludeAsync(requestId);

            await _orderValidator.CheckOrderWithThisIdExists(orderRequest.OrderId);
            await _orderValidator.CheckOrderOfThisCustomer(orderRequest.OrderId, customerId);
            var orderInProcess = await _orderDataService.IsOrderWithIdInProcessAsync(orderRequest.OrderId);

            if (orderRequest.Approved == true && orderInProcess)
            {
                throw new BusinessLogicException("Sorry but this delivery already performs this order");
            }

            await _orderRequestDataService.RejectOrderRequestAsync(requestId);
        }

        public async Task DeleteOrderRequest(int requestId, int deliveryId)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);
            await _orderRequestValidator.CheckOrderRequestWithThisIdExists(requestId);
            await _orderRequestValidator.CheckThisDeliveryHaveAccessFoRThisOrderRequest(requestId, deliveryId);

            var orderRequest = await _orderRequestDataService.GetOrderRequestByIdWithoutIncludeAsync(requestId);
            await _orderValidator.CheckOrderWithThisIdExists(orderRequest.OrderId);
            
            var orderInProcess = await _orderDataService.IsOrderWithIdInProcessAsync(orderRequest.OrderId);

            if (orderRequest.Approved == true && orderInProcess)
            {
                throw new BusinessLogicException("Sorry but you already performs this order");
            }

            await _orderRequestDataService.DeleteOrderRequestAsync(requestId);
        }

        public async Task DeleteOrderRequestLikeCustomer(int requestId, int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderRequestValidator.CheckOrderRequestWithThisIdExists(requestId);

            var orderRequest = await _orderRequestDataService.GetOrderRequestByIdWithoutIncludeAsync(requestId);

            await _orderValidator.CheckOrderOfThisCustomer(orderRequest.OrderId, customerId);
            await _orderValidator.CheckOrderWithThisIdExists(orderRequest.OrderId);

            var orderInProcess = await _orderDataService.IsOrderWithIdInProcessAsync(orderRequest.OrderId);

            if (orderRequest.Approved == true && orderInProcess)
            {
                throw new BusinessLogicException("Sorry but this delivery already performs this order");
            }

            await _orderRequestDataService.DeleteOrderRequestAsync(requestId);
        }

        public async Task<List<OrderRequestViewData>> GetMyRequests(int deliveryId)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);

            var orderRequest = await _orderRequestDataService.GetMyOrderRequestsAsync(deliveryId);

            return orderRequest;
        }

        public async Task<List<OrderRequestViewData>> GetOrderRequests(int orderId, int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderValidator.CheckOrderWithThisIdExists(orderId);
            await _orderValidator.CheckOrderOfThisCustomer(orderId, customerId);

            var orderRequest = await _orderRequestDataService.GetOrderRequestsAsync(orderId);

            return orderRequest;
        }

        public async Task RejectAllOrderRequest(int orderId, int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderValidator.CheckOrderWithThisIdExists(orderId);
            await _orderValidator.CheckOrderOfThisCustomer(orderId, customerId);

            await _orderRequestDataService.RejectAllOrderRequestsAsync(orderId);
        }
    }
}

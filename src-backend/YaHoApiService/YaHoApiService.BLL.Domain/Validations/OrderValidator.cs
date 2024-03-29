﻿using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;

namespace YaHo.YaHoApiService.BLL.Domain.Validations
{
    internal class OrderValidator
    {
        private readonly IOrderDataService _orderDataService;

        public OrderValidator(IOrderDataService orderDataService)
        {
            _orderDataService = orderDataService;
        }


        public async Task CheckOrderWithThisIdExists(int orderId)
        {
            if (!await _orderDataService.IsOrderWithIdExistsAsync(orderId))
            {
                throw new ValidationException($"No order with this id: '{orderId}'.");
            }
        }

        public async Task CheckThisOrderNoOneApproved(int orderId)
        {
            if (!await _orderDataService.ThisOrderNoOneApprovedAsync(orderId))
            {
                throw new ValidationException($"Delivery already approved");
            }
        }

        public async Task CheckOrderOfThisCustomer(int orderId, int customerId)
        {
            if (!await _orderDataService.OrderOfThisCustomerAsync(orderId, customerId))
            {
                throw new ValidationException("You can't update this order.");
            }
        }

        public async Task CheckOrderStatusInProcess(int orderId)
        {
            if (!await _orderDataService.IsOrderWithIdInProcessAsync(orderId))
            {
                throw new ValidationException($"Order not executing, no sense to make a confirmation '{orderId}'.");
            }
        }

        public async Task CheckOrderStatusNotInProcess(int orderId)
        {
            if (await _orderDataService.IsOrderWithIdInProcessAsync(orderId))
            {
                throw new ValidationException($"Order in process you cant change anything");
            }
        }


        public async Task CheckOrderStatusDone(int orderId)
        {
            if (!await _orderDataService.IsOrderWithIdDoneAsync(orderId))
            {
                throw new ValidationException($"Order completed its cannot be changed.");
            }
        }

        public async Task CheckOrderStatusNotDone(int orderId)
        {
            if (await _orderDataService.IsOrderWithIdDoneAsync(orderId))
            {
                throw new ValidationException($"Order done you cant change anything");
            }
        }

        public async Task CheckOrderStatusInCreating(int orderId)
        {
            if (!await _orderDataService.IsOrderWithIdInCreatingAsync(orderId))
            {
                throw new ValidationException($"Order not in creating, change order status to creating.");
            }
        }

        public async Task CheckOrderStatusInExpectation(int orderId)
        {
            if (!await _orderDataService.IsOrderWithIdInExpectationAsync(orderId))
            {
                throw new ValidationException($"Order not in expectation");
            }
        }

        public async Task CheckThisUserHaveAccess(int orderId, string userId)
        {
            if (!await _orderDataService.ThisUserHaveAccessAsync(orderId, userId))
            {
                throw new ValidationException($"You don't have access to this information..");
            }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;
using YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm;
using YaHo.YaHoApiService.BLL.Domain.Validations;
using YaHo.YaHoApiService.Common.Exceptions;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class ConfirmService : IConfirmService
    {
        private readonly IConfirmDataService _confirmDataService;

        private readonly IOrderDataService _orderDataService;

        private readonly IUserDataService _userDataService;

        private readonly DeliveryValidator _deliveryValidator;

        private readonly UserValidator _userValidator;

        private readonly CustomerValidator _customerValidator;

        private readonly OrderValidator _orderValidator;

        private readonly ConfirmValidator _confirmValidator;

        public ConfirmService(IOrderDataService orderDataService,
            IUserDataService userDataService,
            IDeliveryDataService deliveryDataService,
            IConfirmDataService confirmDataService,
            ICustomerDataService customerDataService)
        {
            _orderDataService = orderDataService;
            _confirmDataService = confirmDataService;
            _confirmValidator = new ConfirmValidator(confirmDataService);
            _userDataService = userDataService;
            _userValidator = new UserValidator(userDataService);
            _deliveryValidator = new DeliveryValidator(deliveryDataService);
            _customerValidator = new CustomerValidator(customerDataService);
            _orderValidator = new OrderValidator(orderDataService);
        }

        #region ConfirmDeliveryCharge

        public async Task CreateConfirmChangeDeliveryCharge(CreateConfirmDeliveryChargeViewData model, string userId,
            int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _userValidator.CheckUserWithThisIdExists(userId);
            await _orderValidator.CheckOrderWithThisIdExists(model.OrderId);
            await _orderValidator.CheckOrderOfThisCustomer(model.OrderId, customerId);
            await _orderValidator.CheckOrderStatusInProcess(model.OrderId);
            await _confirmValidator.AnyDeliveryChargeActiveConfirm(model.OrderId);

            var order = await _orderDataService.GetOrderByIdForCustomerAsync(model.OrderId);
            model.PreviousPrice = order.DeliveryCharge;
            model.InitialDate = DateTime.UtcNow;
            model.CustomerConfirm = true;

            if (model.NewPrice > model.PreviousPrice)
            {
                await _userValidator.CheckUserHasEnoughMoney(userId, model.NewPrice - model.PreviousPrice);
                var result = await _userDataService.FreezeMoneyAsync(userId, model.NewPrice - model.PreviousPrice);

                if (!result)
                {
                    throw new BusinessLogicException("There were problems during the debit from the account, the confirm was not created.");
                }
            }

            await _confirmDataService.CreateConfirmForDeliveryChargeAsync(model);
        }

        public async Task DeleteConfirmChangeDeliveryCharge(int id, string userId,
            int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _userValidator.CheckUserWithThisIdExists(userId);
            await _confirmValidator.CheckConfirmDeliveryChargeExists(id);
            await _confirmValidator.CheckConfirmDeliveryChargeOfThisCustomer(id, customerId);

            var confirmDelete = await _confirmDataService.GetConfirmDeliveryChargeByIdAsync(id);

            var canDelete = CanDeleteConfirmDeliveryCharge(confirmDelete);

            if (canDelete)
            {
                var deleteResult = await _confirmDataService.DeleteConfirmDeliveryChargeAsync(confirmDelete.Id);

                if (deleteResult && confirmDelete.NewPrice > confirmDelete.PreviousPrice)
                {
                    await _userDataService.DefrostMoneyAsync(userId, confirmDelete.NewPrice - confirmDelete.PreviousPrice);
                }
            }
            else
            {
                throw new BusinessLogicException("Sorry, but you can’t delete this confirmation anymore" +
                                                 ", because the system or the delivery has already confirmed the action");
            }
        }

        public async Task<List<ConfirmDeliveryChargeViewData>> GetConfirmsDeliveryCharge(int orderId, string userId)
        {
            await _userValidator.CheckUserWithThisIdExists(userId);
            await _orderValidator.CheckOrderWithThisIdExists(orderId);
            await _orderValidator.CheckOrderStatusInProcess(orderId);
            await _orderValidator.CheckThisUserHaveAccess(orderId, userId);

            var confirms = await _confirmDataService.GetConfirmsDeliveryChargeForOrderAsync(orderId);

            return confirms;
        }

        public async Task UpdateConfirmDeliveryCharge(int id, int deliveryId, string userId, bool deliveryConfirm)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);
            await _confirmValidator.CheckConfirmDeliveryChargeExists(id);
            await _confirmValidator.CheckConfirmDeliveryChargeNotAnswered(id);
            await _confirmValidator.CheckThisDeliveryHaveAccessToDeliveryCharge(id, deliveryId);

            var confirmGet = await _confirmDataService.GetConfirmDeliveryChargeByIdAsync(id);

            await ConfirmationResponseDeliveryCharge(deliveryConfirm, confirmGet);
            await _confirmDataService.UpdateConfirmDeliveryChargeAsync(id, deliveryConfirm);
        }

        #endregion


        #region ConfirmExpectedDate

        public async Task CreateConfirmConfirmExpectedDateLikeCustomer(CreateConfirmExpectedDateViewData model, int customerId, string userId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _orderValidator.CheckOrderWithThisIdExists(model.OrderId);
            await _orderValidator.CheckOrderOfThisCustomer(model.OrderId, customerId);
            await _orderValidator.CheckOrderStatusInProcess(model.OrderId);
            await _confirmValidator.AnyExpectedDateActiveConfirm(model.OrderId);

            var order = await _orderDataService.GetOrderByIdAsync(model.OrderId);
            model.PreviousExpectedDate = order.ExpectedDate;
            model.InitialDate = DateTime.UtcNow;
            model.CustomerConfirm = true;
            model.CreaterId = userId;

            await _confirmDataService.CreateConfirmForExpectedDateAsync(model);
        }

        public async Task CreateConfirmConfirmExpectedDateLikeDelivery(CreateConfirmExpectedDateViewData model, string userId)
        {
            await _userValidator.CheckUserWithThisIdExists(userId);
            await _orderValidator.CheckOrderWithThisIdExists(model.OrderId);
            await _orderValidator.CheckThisUserHaveAccess(model.OrderId, userId);
            await _orderValidator.CheckOrderStatusInProcess(model.OrderId);
            await _confirmValidator.AnyExpectedDateActiveConfirm(model.OrderId);

            var order = await _orderDataService.GetOrderByIdAsync(model.OrderId);
            model.PreviousExpectedDate = order.ExpectedDate;
            model.InitialDate = DateTime.UtcNow;
            model.DeliveryConfirm = true;
            model.CreaterId = userId;

            await _confirmDataService.CreateConfirmForExpectedDateAsync(model);
        }

        public async Task DeleteConfirmChangeExpectedDate(int id, string userId)
        {
            await _userValidator.CheckUserWithThisIdExists(userId);
            await _confirmValidator.CheckConfirmExpectedDateExists(id);
            await _confirmValidator.CheckThisUserHaveAccessToDelete(id, userId);

            var confirmDelete = await _confirmDataService.GetConfirmExpectedDateByIdAsync(id);

            var canDelete = CanDeleteConfirmExpectedDate(confirmDelete);

            if (canDelete)
            {
                var deleteResult = await _confirmDataService.DeleteConfirmExpectedDateAsync(confirmDelete.Id);
            }
            else
            {
                throw new BusinessLogicException("Sorry, but you can’t delete this confirmation anymore" +
                                                 ", because the system or the delivery has already confirmed the action");
            }
        }

        public async Task<List<ConfirmExpectedDateViewData>> GetConfirmsExpectedDate(int orderId, string userId)
        {
            await _userValidator.CheckUserWithThisIdExists(userId);
            await _orderValidator.CheckOrderWithThisIdExists(orderId);
            await _orderValidator.CheckOrderStatusInProcess(orderId);
            await _orderValidator.CheckThisUserHaveAccess(orderId, userId);

            var confirms = await _confirmDataService.GetConfirmsExpectedDateForOrderAsync(orderId);

            return confirms;
        }

        public async Task UpdateConfirmExpectedDateLikeDelivery(int id, int deliveryId, bool deliveryConfirm)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);
            await _confirmValidator.CheckConfirmExpectedDateExists(id);
            await _confirmValidator.CheckConfirmExpectedDateNotAnswered(id);
            await _confirmValidator.CheckThisDeliveryHaveAccessToExpectedDate(id, deliveryId);

            var confirmGet = await _confirmDataService.GetConfirmExpectedDateByIdAsync(id);

            if (deliveryConfirm)
            {
                await _orderDataService.UpdateOrderExpectedDateAsync(confirmGet.OrderId, confirmGet.NewExpectedDate);
            }

            await _confirmDataService.UpdateConfirmExpectedDateDeliveryAsync(id, deliveryConfirm);
        }

        public async Task UpdateConfirmExpectedDateLikeCustomer(int id, int customerId, bool customerConfirm)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);
            await _confirmValidator.CheckConfirmExpectedDateExists(id);
            await _confirmValidator.CheckConfirmExpectedDateNotAnswered(id);
            await _confirmValidator.CheckThisCustomerHaveAccessToExpectedDate(id, customerId);

            var confirmGet = await _confirmDataService.GetConfirmExpectedDateByIdAsync(id);

            if (customerConfirm)
            {
                await _orderDataService.UpdateOrderExpectedDateAsync(confirmGet.OrderId, confirmGet.NewExpectedDate);
            }

            await _confirmDataService.UpdateConfirmExpectedDateCustomerAsync(id, customerConfirm);
        }
        
        #endregion


        #region Private_method

        private bool CanDeleteConfirmDeliveryCharge(ConfirmDeliveryChargeViewData model)
            {
                if (!model.AutomaticConfirm.HasValue &&
                    !model.DeliveryConfirm.HasValue)
                {
                    return true;
                }

                return false;
            }

        private bool CanDeleteConfirmExpectedDate(ConfirmExpectedDateViewData model)
        {
            if (!model.AutomaticConfirm.HasValue && 
                (!model.DeliveryConfirm.HasValue || 
                 !model.CustomerConfirm.HasValue))
            {
                return true;
            }

            return false;
        }

        private async Task ConfirmationResponseDeliveryCharge(bool confirm, 
            ConfirmDeliveryChargeViewData confirmGet)
        {
            var order = await _orderDataService.GetOrderByIdWithoutIncludeAsync(confirmGet.OrderId);

            if (!confirm)
            {
                await _userDataService.DefrostMoneyAsync(confirmGet.Order.Customer.UserId,
                    Math.Abs(confirmGet.PreviousPrice - confirmGet.NewPrice));
            }
            else
            {
                if (confirmGet.PreviousPrice < confirmGet.NewPrice)
                {
                    order.DeliveryCharge = confirmGet.NewPrice;
                    await _orderDataService.UpdateOrderAsync(order);
                }
                else if (confirmGet.PreviousPrice > confirmGet.NewPrice)
                {
                    await _userDataService.DefrostMoneyAsync(order.Customer.UserId, confirmGet.PreviousPrice - confirmGet.NewPrice);

                    order.DeliveryCharge = confirmGet.NewPrice;
                    await _orderDataService.UpdateOrderAsync(order);
                }
            }
        }

        #endregion
    }
}

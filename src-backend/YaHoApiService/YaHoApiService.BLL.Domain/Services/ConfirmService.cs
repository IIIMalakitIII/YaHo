using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;
using YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm;
using YaHo.YaHoApiService.BLL.Domain.Validations;
using YaHo.YaHoApiService.Common.Exceptions;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class ConfirmService : IConfirmService
    {
        private readonly IConfirmDataService _confirmDataService;

        private readonly IOrderDataService _orderDataService;

        private readonly IUserDataService _userDataService;

        private readonly IMapper _mapper;

        private readonly DeliveryValidator _deliveryValidator;

        private readonly UserValidator _userValidator;

        private readonly CustomerValidator _customerValidator;

        private readonly OrderValidator _orderValidator;

        private readonly ConfirmValidator _confirmValidator;

        public ConfirmService(IOrderDataService orderDataService,
            IUserDataService userDataService, IMapper mapper,
            IDeliveryDataService deliveryDataService,
            IConfirmDataService confirmDataService,
            ICustomerDataService customerDataService)
        {
            _orderDataService = orderDataService;
            _confirmDataService = confirmDataService;
            _mapper = mapper;
            _confirmValidator = new ConfirmValidator(confirmDataService);
            _userDataService = userDataService;
            _userValidator = new UserValidator(userDataService);
            _deliveryValidator = new DeliveryValidator(deliveryDataService);
            _customerValidator = new CustomerValidator(customerDataService);
            _orderValidator = new OrderValidator(orderDataService);
        }

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

        public async Task UpdateConfirmDeliveryCharge(int id, int deliveryId, bool deliveryConfirm)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);
            await _confirmValidator.CheckConfirmDeliveryChargeExists(id);
            await _confirmValidator.CheckConfirmDeliveryChargeNotAnswered(id);
            await _confirmValidator.CheckThisDeliveryHaveAccessToDeliveryCharge(id, deliveryId);

            await _confirmDataService.UpdateConfirmDeliveryChargeAsync(id, deliveryConfirm);
        }

        

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

        #endregion
    }
}

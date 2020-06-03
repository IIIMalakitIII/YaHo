using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;
using YaHo.YaHoApiService.BLL.Contracts.Interfaces.Confirm;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class ConfirmDataService : IConfirmDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public ConfirmDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        #region ConfirmDeliveryCharge
        public async Task<bool> AnyDeliveryChargeActiveConfirmAsync(int orderId)
        {
            return await _context.ConfirmsDeliveryChargeWithoutTracking
                .Where(x => x.OrderId == orderId)
                .AnyAsync(x =>
                    !x.AutomaticConfirm.HasValue &&
                    !x.DeliveryConfirm.HasValue &&
                    x.InitialDate.AddDays(5) > DateTime.UtcNow);

        }

        public async Task<bool> CheckConfirmDeliveryChargeExistsAsync(int id)
        {
            return await _context.ConfirmsDeliveryChargeWithoutTracking
                .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CheckConfirmDeliveryChargeOfThisCustomerAsync(int id, int customerId)
        {
            var confirm = await _context.ConfirmsDeliveryChargeWithoutTracking
                .Include(x => x.Order)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return confirm.Order.CustomerId == customerId;
        }

        public async Task<bool> CheckThisDeliveryHaveAccessToDeliveryChargeAsync(int id, int deliveryId)
        {
            return await _context.ConfirmsDeliveryChargeWithoutTracking
                .Include(x => x.Order)
                .ThenInclude(x => x.OrderRequests)
                .AnyAsync(x => x.Id == id &&
                               x.Order.OrderRequests.Any(o => o.DeliveryId == deliveryId && o.Approved == true));
        }

        public async Task<bool> CheckConfirmDeliveryChargeNotAnsweredAsync(int id)
        {
            var confirm = await _context.ConfirmsDeliveryChargeWithoutTracking
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return !confirm.AutomaticConfirm.HasValue &&
                   !confirm.DeliveryConfirm.HasValue &&
                   confirm.InitialDate.AddDays(5) > DateTime.UtcNow;
        }

        public async Task CreateConfirmForDeliveryChargeAsync(CreateConfirmDeliveryChargeViewData model)
        {
            var confirmDeliveryChargeDbo = _mapper.Map<ConfirmDeliveryChargeDbo>(model);

            _context.ConfirmsDeliveryCharge.Add(confirmDeliveryChargeDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.ConfirmDeliveryCharge);
            }
        }

        public async Task<ConfirmDeliveryChargeViewData> GetConfirmDeliveryChargeByIdAsync(int id)
        {
            var confirmDbo = await _context.ConfirmsDeliveryChargeWithoutTracking
                .Include(x => x.Order)
                .ThenInclude(x => x.Customer)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var confirmViewData = _mapper.Map<ConfirmDeliveryChargeViewData>(confirmDbo);

            return confirmViewData;
        }

        public async Task<List<ConfirmDeliveryChargeViewData>> GetConfirmsDeliveryChargeForOrderAsync(int orderId)
        {
            var confirmsDbo = await _context.ConfirmsDeliveryChargeWithoutTracking
                .Include(x => x.Order)
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            var confirmsViewData = _mapper.Map<List<ConfirmDeliveryChargeViewData>>(confirmsDbo);

            return confirmsViewData;
        }

        public async Task UpdateConfirmDeliveryChargeAsync(int id, bool deliveryConfirm)
        {
            var confirmDbo = await _context.ConfirmsDeliveryCharge
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            confirmDbo.DeliveryConfirm = deliveryConfirm;

            _context.ConfirmsDeliveryCharge.Update(confirmDbo);

            var result = await _context.TrySaveChangesAsync();

            if (!result)
            {
                throw new BusinessLogicException("Delivery confirme not changed, something is wrong");
            }
        }

        public async Task<bool> DeleteConfirmDeliveryChargeAsync(int confirmId)
        {
            var confirmDbo = await _context.ConfirmsDeliveryCharge
                .Where(x => x.Id == confirmId)
                .FirstOrDefaultAsync();

            _context.ConfirmsDeliveryCharge.Remove(confirmDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new DeleteFailureException(EntityNames.ConfirmDeliveryCharge, confirmId);
            }

            return true;
        }

        #endregion


        #region ConfirmExpectedDate

        public async Task<bool> AnyExpectedDateActiveConfirmAsync(int orderId)
        {
            return await _context.ConfirmsDeliveryChargeWithoutTracking
                .Where(x => x.OrderId == orderId)
                .AnyAsync(x =>
                    !x.AutomaticConfirm.HasValue &&
                    (!x.DeliveryConfirm.HasValue || !x.CustomerConfirm.HasValue) &&
                    x.InitialDate.AddDays(5) > DateTime.UtcNow);

        }

        public async Task<bool> CheckConfirmExpectedDateExistsAsync(int id)
        {
            return await _context.ConfirmsExpectedDateWithoutTracking
                .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CheckThisCustomerHaveAccessToExpectedDateAsync(int id, int customerId)
        {
            var confirm = await _context.ConfirmsExpectedDateWithoutTracking
                .Include(x => x.Order)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return confirm.Order.CustomerId == customerId;
        }

        public async Task<bool> CheckThisDeliveryHaveAccessToExpectedDateAsync(int id, int deliveryId)
        {
            return await _context.ConfirmsExpectedDateWithoutTracking
                .Include(x => x.Order)
                .ThenInclude(x => x.OrderRequests)
                .AnyAsync(x => x.Id == id &&
                               x.Order.OrderRequests.Any(o => o.DeliveryId == deliveryId && o.Approved == true));
        }

        public async Task<bool> CheckConfirmExpectedDateNotAnsweredAsync(int id)
        {
            var confirm = await _context.ConfirmsExpectedDateWithoutTracking
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return !confirm.AutomaticConfirm.HasValue &&
                   !confirm.DeliveryConfirm.HasValue &&
                   !confirm.CustomerConfirm.HasValue &&
                   confirm.InitialDate.AddDays(5) > DateTime.UtcNow;
        }

        public async Task<bool> CheckThisUserHaveAccessToDeleteAsync(int id, string userId)
        {
            var confirm = await _context.ConfirmsExpectedDateWithoutTracking
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return confirm.CreaterId == userId;
        }

        public async Task CreateConfirmForExpectedDateAsync(CreateConfirmExpectedDateViewData model)
        {
            var confirmExpectedDateDbo = _mapper.Map<ConfirmExpectedDateDbo>(model);

            _context.ConfirmsExpectedDate.Add(confirmExpectedDateDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.ConfirmsExpectedDate);
            }
        }

        public async Task<ConfirmExpectedDateViewData> GetConfirmExpectedDateByIdAsync(int id)
        {
            var confirmDbo = await _context.ConfirmsExpectedDateWithoutTracking
                .Include(x => x.Order)
                .ThenInclude(x => x.Customer)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var confirmViewData = _mapper.Map<ConfirmExpectedDateViewData>(confirmDbo);

            return confirmViewData;
        }

        public async Task<List<ConfirmExpectedDateViewData>> GetConfirmsExpectedDateForOrderAsync(int orderId)
        {
            var confirmsDbo = await _context.ConfirmsExpectedDateWithoutTracking
                .Include(x => x.Order)
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            var confirmsViewData = _mapper.Map<List<ConfirmExpectedDateViewData>>(confirmsDbo);

            return confirmsViewData;
        }

        public async Task UpdateConfirmExpectedDateDeliveryAsync(int id, bool deliveryConfirm)
        {
            var confirmDbo = await _context.ConfirmsExpectedDate
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            confirmDbo.DeliveryConfirm = deliveryConfirm;

            _context.ConfirmsExpectedDate.Update(confirmDbo);

            var result = await _context.TrySaveChangesAsync();

            if (!result)
            {
                throw new BusinessLogicException("Delivery confirme not changed, something is wrong");
            }
        }

        public async Task UpdateConfirmExpectedDateCustomerAsync(int id, bool customerConfirm)
        {
            var confirmDbo = await _context.ConfirmsExpectedDate
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            confirmDbo.CustomerConfirm = customerConfirm;

            _context.ConfirmsExpectedDate.Update(confirmDbo);

            var result = await _context.TrySaveChangesAsync();

            if (!result)
            {
                throw new BusinessLogicException("Delivery confirme not changed, something is wrong");
            }
        }

        public async Task<bool> DeleteConfirmExpectedDateAsync(int confirmId)
        {
            var confirmDbo = await _context.ConfirmsExpectedDate
                .Where(x => x.Id == confirmId)
                .FirstOrDefaultAsync();

            _context.ConfirmsExpectedDate.Remove(confirmDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new DeleteFailureException(EntityNames.ConfirmsExpectedDate, confirmId);
            }

            return true;
        }

        #endregion


        #region ConfirmOrderStatus

        public async Task<bool> AnyOrderStatusActiveConfirmAsync(int orderId)
        {
            return await _context.ConfirmsOrderStatusWithoutTracking
                .Where(x => x.OrderId == orderId)
                .AnyAsync(x =>
                    !x.AutomaticConfirm.HasValue &&
                    (!x.DeliveryConfirm.HasValue || !x.CustomerConfirm.HasValue) &&
                    x.InitialDate.AddDays(5) > DateTime.UtcNow);

        }

        public async Task<bool> CheckConfirmOrderStatusExistsAsync(int id)
        {
            return await _context.ConfirmsOrderStatusWithoutTracking
                .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CheckThisCustomerHaveAccessToOrderStatusAsync(int id, int customerId)
        {
            var confirm = await _context.ConfirmsOrderStatusWithoutTracking
                .Include(x => x.Order)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return confirm.Order.CustomerId == customerId;
        }

        public async Task<bool> CheckThisDeliveryHaveAccessToOrderStatusAsync(int id, int deliveryId)
        {
            return await _context.ConfirmsOrderStatusWithoutTracking
                .Include(x => x.Order)
                .ThenInclude(x => x.OrderRequests)
                .AnyAsync(x => x.Id == id &&
                               x.Order.OrderRequests.Any(o => o.DeliveryId == deliveryId && o.Approved == true));
        }

        public async Task<bool> CheckConfirmOrderStatusNotAnsweredAsync(int id)
        {
            var confirm = await _context.ConfirmsOrderStatusWithoutTracking
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return !confirm.AutomaticConfirm.HasValue &&
                   (!confirm.DeliveryConfirm.HasValue ||
                   !confirm.CustomerConfirm.HasValue) &&
                   confirm.InitialDate.AddDays(5) > DateTime.UtcNow;
        }

        public async Task<bool> CheckThisUserHaveAccessToDeleteOrderStatusAsync(int id, string userId)
        {
            var confirm = await _context.ConfirmsOrderStatusWithoutTracking
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return confirm.CreaterId == userId;
        }

        public async Task CreateConfirmForOrderStatusAsync(CreateConfirmOrderStatusViewData model)
        {
            var confirmExpectedDateDbo = _mapper.Map<ConfirmOrderStatusDbo>(model);

            _context.ConfirmsOrderStatus.Add(confirmExpectedDateDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.ConfirmsOrderStatus);
            }
        }

        public async Task<ConfirmOrderStatusViewData> GetConfirmOrderStatusByIdAsync(int id)
        {
            var confirmDbo = await _context.ConfirmsOrderStatusWithoutTracking
                .Include(x => x.Order)
                .ThenInclude(x => x.Customer)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var confirmViewData = _mapper.Map<ConfirmOrderStatusViewData>(confirmDbo);

            return confirmViewData;
        }

        public async Task<List<ConfirmOrderStatusViewData>> GetConfirmsOrderStatusForOrderAsync(int orderId)
        {
            var confirmsDbo = await _context.ConfirmsOrderStatusWithoutTracking
                .Include(x => x.Order)
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            var confirmsViewData = _mapper.Map<List<ConfirmOrderStatusViewData>>(confirmsDbo);

            return confirmsViewData;
        }

        public async Task UpdateConfirmOrderStatusDeliveryAsync(int id, bool deliveryConfirm)
        {
            var confirmDbo = await _context.ConfirmsOrderStatus
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            confirmDbo.DeliveryConfirm = deliveryConfirm;

            _context.ConfirmsOrderStatus.Update(confirmDbo);

            var result = await _context.TrySaveChangesAsync();

            if (!result)
            {
                throw new BusinessLogicException("Delivery confirme not changed, something is wrong");
            }
        }

        public async Task UpdateConfirmOrderStatusCustomerAsync(int id, bool customerConfirm)
        {
            var confirmDbo = await _context.ConfirmsOrderStatus
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            confirmDbo.CustomerConfirm = customerConfirm;

            _context.ConfirmsOrderStatus.Update(confirmDbo);

            var result = await _context.TrySaveChangesAsync();

            if (!result)
            {
                throw new BusinessLogicException("Delivery confirm not changed, something is wrong");
            }
        }

        public async Task<bool> DeleteConfirmOrderStatusAsync(int confirmId)
        {
            var confirmDbo = await _context.ConfirmsOrderStatus
                .Where(x => x.Id == confirmId)
                .FirstOrDefaultAsync();

            _context.ConfirmsOrderStatus.Remove(confirmDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new DeleteFailureException(EntityNames.ConfirmsOrderStatus, confirmId);
            }

            return true;
        }

        #endregion
    }
}

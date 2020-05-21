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

        public async Task CreateConfirmForDeliveryChargeAsync(CreateConfirmDeliveryChargeViewData model)
        {
            var confirmDeliveryChargeDbo = _mapper.Map<ConfirmDeliveryChargeDbo>(model);

            _context.ConfirmDeliveryCharges.Add(confirmDeliveryChargeDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.ConfirmDeliveryCharge);
            }
        }

        public async Task<bool> AnyDeliveryChargeActiveConfirmAsync(int orderId)
        {
            return await _context.ConfirmDeliveryChargesWithoutTracking
                .Where(x => x.OrderId == orderId)
                .AnyAsync(x =>
                    !x.AutomaticConfirm.HasValue &&
                    !x.DeliveryConfirm.HasValue &&
                    x.InitialDate.AddDays(5) > DateTime.UtcNow);

        }

        public async Task<bool> CheckConfirmDeliveryChargeExistsAsync(int id)
        {
            return await _context.ConfirmDeliveryChargesWithoutTracking
                .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CheckConfirmDeliveryChargeOfThisCustomerAsync(int id, int customerId)
        {
            var confirm = await _context.ConfirmDeliveryChargesWithoutTracking
                .Include(x => x.Order)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return confirm.Order.CustomerId == customerId;
        }

        public async Task<bool> CheckThisDeliveryHaveAccessToDeliveryChargeAsync(int id, int deliveryId)
        {
            return await _context.ConfirmDeliveryChargesWithoutTracking
                .Include(x => x.Order)
                .ThenInclude(x => x.OrderRequests)
                .AnyAsync(x => x.Id == id &&
                          x.Order.OrderRequests.Any(o => o.DeliveryId == deliveryId && o.Approved == true));
        }

        public async Task<bool> CheckConfirmDeliveryChargeNotAnsweredAsync(int id)
        {
            var confirm = await _context.ConfirmDeliveryChargesWithoutTracking
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return !confirm.AutomaticConfirm.HasValue &&
                   !confirm.DeliveryConfirm.HasValue &&
                   confirm.InitialDate.AddDays(5) > DateTime.UtcNow;
        }

        public async Task<ConfirmDeliveryChargeViewData> GetConfirmDeliveryChargeByIdAsync(int id)
        {
            var confirmDbo = await _context.ConfirmDeliveryChargesWithoutTracking
                .Include(x => x.Order)
                    .ThenInclude(x => x.Customer)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var confirmViewData = _mapper.Map<ConfirmDeliveryChargeViewData>(confirmDbo);

            return confirmViewData;
        }

        public async Task<List<ConfirmDeliveryChargeViewData>> GetConfirmsDeliveryChargeForOrderAsync(int orderId)
        {
            var confirmsDbo = await _context.ConfirmDeliveryChargesWithoutTracking
                .Include(x => x.Order)
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            var confirmsViewData = _mapper.Map<List<ConfirmDeliveryChargeViewData>>(confirmsDbo);

            return confirmsViewData;
        }

        public async Task UpdateConfirmDeliveryChargeAsync(int id, bool deliveryConfirm)
        {
            var confirmDbo = await _context.ConfirmDeliveryCharges
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            confirmDbo.DeliveryConfirm = deliveryConfirm;

            _context.Update(confirmDbo);

            var result = await _context.TrySaveChangesAsync();

            if (!result)
            {
                throw new BusinessLogicException("Delivery confirme not changed, something is wrong");
            }
        }

        public async Task<bool> DeleteConfirmDeliveryChargeAsync(int confirmId)
        {
            var confirmDbo = await _context.ConfirmDeliveryCharges
                .Where(x => x.Id == confirmId)
                .FirstOrDefaultAsync();

            _context.ConfirmDeliveryCharges.Remove(confirmDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new DeleteFailureException(EntityNames.ConfirmDeliveryCharge, confirmId);
            }

            return true;
        }
    }
}

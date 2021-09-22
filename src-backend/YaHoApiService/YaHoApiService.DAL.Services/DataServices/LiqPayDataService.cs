using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.LiqPayOrder;
using YaHo.YaHoApiService.BLL.Contracts.Interfaces.LiqPay;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class LiqPayDataService : ILiqPayDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public LiqPayDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> IsLiqPayOrderWithIdExistsAsync(string id)
        {
            return await _context.LiqPayOrdersWithoutTracking.AnyAsync(x => x.LiqPayOrderId == id);
        }

        public async Task<string> CreateLiqPayOrderAsync(decimal money, string userId)
        {
            var liqPayOrderDbo = new LiqPayOrderDbo()
            {
                Money = money,
                UserId = userId,
                InitialDate = DateTime.UtcNow
            };

            _context.LiqPayOrders.Add(liqPayOrderDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.LiqPayOrder);
            }

            return liqPayOrderDbo.LiqPayOrderId;
        }

        public async Task<LiqPayOrderViewData> GetLiqPayOrderAsync(string id)
        {
            var liqPayOrder = await _context.LiqPayOrdersWithoutTracking
                .Where(x => x.LiqPayOrderId == id)
                .FirstOrDefaultAsync();

            var liqPayOrderViewData = _mapper.Map<LiqPayOrderViewData>(liqPayOrder);

            return liqPayOrderViewData;
        }
    }
}

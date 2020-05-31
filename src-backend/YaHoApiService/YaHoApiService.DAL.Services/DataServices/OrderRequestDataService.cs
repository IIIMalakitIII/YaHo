using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.OrderRequest;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class OrderRequestDataService : IOrderRequestDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public OrderRequestDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> IsOrderRequestWithIdExistsAsync(int requestId)
        {
            return await _context.OrderRequestsWithoutTracking
                .AnyAsync(x => x.OrderRequestId == requestId);
        }

        public async Task<bool> ThisDeliveryHaveAccessFoRThisOrderRequest(int requestId, int deliveryId)
        {
            var request=  await _context.OrderRequestsWithoutTracking
                .Where(x => x.OrderRequestId == requestId)
                .FirstOrDefaultAsync();

            return request.DeliveryId == deliveryId;
        }

        public async Task<bool> SomeRequestHasAlreadyBeenApproved(int orderId)
        {
            return await _context.OrderRequestsWithoutTracking
                .AnyAsync(x => x.OrderId == orderId && x.Approved == true);
        }

        public async Task CreateOrderRequestAsync(OrderRequestViewData model)
        {
            var orderRequestDbo = _mapper.Map<OrderRequestDbo>(model);

            _context.OrderRequests.Add(orderRequestDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.OrderRequest);
            }
        }

        public async Task ApproveOrderRequestAsync(int requestId)
        {
            var orderRequestDbo = await _context.OrderRequests
                .Where(x => x.OrderRequestId == requestId)
                .FirstOrDefaultAsync();

            orderRequestDbo.Approved = true;

            _context.OrderRequests.Update(orderRequestDbo);

            await _context.SaveChangesAsync();
        }

        public async Task RejectOrderRequestAsync(int requestId)
        {
            var orderRequestDbo = await _context.OrderRequests
                .Where(x => x.OrderRequestId == requestId)
                .FirstOrDefaultAsync();

            orderRequestDbo.Approved = false;

            _context.OrderRequests.Update(orderRequestDbo);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderRequestAsync(int requestId)
        {
            var orderRequestDbo = await _context.OrderRequests
                .Where(x => x.OrderRequestId == requestId)
                .FirstOrDefaultAsync();

            _context.OrderRequests.Remove(orderRequestDbo);

            await _context.SaveChangesAsync();
        }

        public async Task<OrderRequestViewData> GetOrderRequestByIdWithoutIncludeAsync(int requestId)
        {
            var orderRequestDbo = await _context.OrderRequestsWithoutTracking
                .Where(x => x.OrderRequestId == requestId)
                .FirstOrDefaultAsync();

            var orderRequestViewData = _mapper.Map<OrderRequestViewData>(orderRequestDbo);

            return orderRequestViewData;
        }

        public async Task<List<OrderRequestViewData>> GetMyOrderRequestsAsync(int deliveryId)
        {
            var ordersRequestsDbo = await _context.OrderRequestsWithoutTracking
                .Where(x => x.DeliveryId == deliveryId)
                .ToListAsync();

            var orderRequestsViewData = _mapper.Map<List<OrderRequestViewData>>(ordersRequestsDbo);

            return orderRequestsViewData;
        }

        public async Task<List<OrderRequestViewData>> GetOrderRequestsAsync(int orderId)
        {
            var orderRequestsDbo = await _context.OrderRequestsWithoutTracking
                .Include(x => x.Delivery)
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            var orderRequestsViewData = _mapper.Map<List<OrderRequestViewData>>(orderRequestsDbo);

            return orderRequestsViewData;
        }

        public async Task RejectAllOrderRequestsAsync(int orderId)
        {
            var orderRequestsDbo = await _context.OrderRequestsWithoutTracking
                .Where(x => x.OrderId == orderId && x.Approved != true)
                .ToListAsync();

            orderRequestsDbo.ForEach(x => x.Approved = false);

            _context.OrderRequests.UpdateRange(orderRequestsDbo);

            await _context.SaveChangesAsync();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order.Update;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Enums;
using YaHo.YaHoApiService.DAL.Services.Context;
using YaHo.YaHoApiService.DAL.Services.QueryHelper;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class OrderDataService : IOrderDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public OrderDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> IsOrderWithIdExistsAsync(int id)
        {
            return await _context.OrdersWithoutTracking.AnyAsync(x => x.OrderId == id);
        }

        public async Task<bool> OrderOfThisCustomerAsync(int orderId, int customerId)
        {
            var order = await _context.OrdersWithoutTracking.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();

            return order.CustomerId == customerId;
        }

        public async Task<bool> IsOrderWithIdInProcessAsync(int orderId)
        {
            var order = await _context.OrdersWithoutTracking.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();

            return order.OrderStatus == OrderStatus.InProcess;
        }

        public async Task<bool> IsOrderWithIdInExpectationAsync(int orderId)
        {
            var order = await _context.OrdersWithoutTracking.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();

            return order.OrderStatus == OrderStatus.InExpectation;
        }

        public async Task<bool> IsOrderWithIdInCreatingAsync(int orderId)
        {
            var order = await _context.OrdersWithoutTracking.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();

            return order.OrderStatus == OrderStatus.Creating;
        }

        public async Task<bool> ThisUserHaveAccessAsync(int orderId, string userId)
        {
            return await _context.OrdersWithoutTracking
                .Include(x => x.Customer)
                .Include(x => x.OrderRequests)
                .ThenInclude(x => x.Delivery)
                .ThenInclude(x => x.User)
                .AnyAsync(x => x.OrderId == orderId &&
                               (x.Customer.User.Id == userId ||
                                (x.OrderRequests.Any(u => u.Delivery.UserId == userId &&
                                                          u.Approved == true))));
        }

        public async Task<int> CreateOrderAsync(OrderViewData model)
        {
            var orderDbo = _mapper.Map<OrderDbo>(model);

            _context.Orders.Add(orderDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.Order);
            }

            return orderDbo.OrderId;
        }

        public async Task DeleteOrderAfterFailureAsync(int orderId)
        {
            var orderDbo = await _context.Orders
                    .FirstOrDefaultAsync(x => x.OrderId == orderId);

            _context.Orders.Remove(orderDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new DeleteFailureException(EntityNames.Order, orderId);
            }
        }

        public async Task<List<OrderViewData>> GetOrdersByFilter(OrderFilterViewData filter)
        {

            var orders = await _context.OrdersWithoutTracking
                .Include(x => x.Customer)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Media)
                .Where(x => x.OrderStatus == OrderStatus.InProcess)
                .FilterBy(filter.DeliveryСountry,
                    filter.DeliveryCity,
                    filter.DeliveryAddress,
                    filter.DeliveryFromCity,
                    filter.DeliveryFromСountry,
                    filter.Filter,
                    filter.Bargain,
                    filter.ExpectedDateFrom,
                    filter.ExpectedDateTo)
                .ToListAsync();

            var orderViewData = _mapper.Map<List<OrderViewData>>(orders);

            return orderViewData;
        }


        public async Task<OrderViewData> GetOrderByIdForCustomerAsync(int orderId)
        {
            
            var order = await _context.OrdersWithoutTracking
                .Include(x => x.Customer)
                .Include(x => x.OrderRequests)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Media)
                .Where(x => x.OrderId == orderId)
                .FirstOrDefaultAsync();

            var orderViewData = _mapper.Map<OrderViewData>(order);

            return orderViewData;
        }

        public async Task<OrderViewData> GetOrderByIdWithoutIncludeAsync(int orderId)
        {

            var order = await _context.OrdersWithoutTracking
                .Where(x => x.OrderId == orderId)
                .FirstOrDefaultAsync();

            var orderViewData = _mapper.Map<OrderViewData>(order);

            return orderViewData;
        }

        public async Task<OrderViewData> GetOrderByIdAsync(int orderId)
        {

            var order = await _context.OrdersWithoutTracking
                .Include(x => x.Customer)
                    .ThenInclude(x => x.User)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Media)
                .Where(x => x.OrderId == orderId && x.OrderStatus == OrderStatus.InProcess)
                .FirstOrDefaultAsync();

            var orderViewData = _mapper.Map<OrderViewData>(order);

            return orderViewData;
        }

        public async Task<List<OrderViewData>> GetOrdersForCustomerAsync(int customerId)
        {

            var orders = await _context.OrdersWithoutTracking
                .Include(x => x.Customer)
                .Include(x => x.OrderRequests)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Media)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            var ordersViewData = _mapper.Map<List<OrderViewData>>(orders);

            return ordersViewData;
        }

        public async Task<List<OrderViewData>> GetOrdersForDeliveryAsync(int deliveryId)
        {
            var orders = await _context.OrdersWithoutTracking
                .Include(x => x.Customer)
                .Include(x => x.Products)
                    .ThenInclude(x => x.Media)
                .Where(x => x.OrderRequests
                    .Any(u => u.DeliveryId == deliveryId && u.Approved == true))
                .ToListAsync();

            var ordersViewData = _mapper.Map<List<OrderViewData>>(orders);

            return ordersViewData;
        }

        public async Task UpdateOrderAsync(OrderViewData order)
        {
            var orderDbo = await _context.Orders
                .FirstOrDefaultAsync(x => x.OrderId == order.OrderId);

            if (orderDbo is null)
            {
                throw new NotFoundException(EntityNames.Order, order.OrderId);
            }

            _mapper.Map(order, orderDbo);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderInfoAsync(UpdateOrderViewData order)
        {
            var orderDbo = await _context.Orders
                .FirstOrDefaultAsync(x => x.OrderId == order.OrderId);

            if (orderDbo is null)
            {
                throw new NotFoundException(EntityNames.Order, order.OrderId);
            }

            _mapper.Map(order, orderDbo);

            await _context.SaveChangesAsync();
        }
    }
}

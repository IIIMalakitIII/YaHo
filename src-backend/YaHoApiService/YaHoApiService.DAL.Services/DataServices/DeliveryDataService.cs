using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class DeliveryDataService: IDeliveryDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public DeliveryDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> IsDeliveryWithIdExistsAsync(int id)
        {
            return await _context.DeliveriesWithoutTracking.AnyAsync(x => x.DeliveryId == id);
        }


        public async Task<DeliveryViewData> GetDeliveryAsync(int id)
        {
            var deliveryDbo = await _context.DeliveriesWithoutTracking
                .Include(x => x.User)
                .Where(x => x.DeliveryId == id)
                .FirstOrDefaultAsync();

            var deliveryViewData = _mapper.Map<DeliveryViewData>(deliveryDbo);

            return deliveryViewData;
        }

        public async Task<DeliveryViewData> GetDeliveryWithoutIncludeAsync(int id)
        {
            var deliveryDbo = await _context.DeliveriesWithoutTracking
                .Where(x => x.DeliveryId == id)
                .FirstOrDefaultAsync();

            var deliveryViewData = _mapper.Map<DeliveryViewData>(deliveryDbo);

            return deliveryViewData;
        }

        public async Task<DeliveryViewData> GetCustomerByUserIdAsync(string id)
        {
            var deliveryDbo = await _context.DeliveriesWithoutTracking
                .Include(x => x.User)
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();

            var deliveryViewData = _mapper.Map<DeliveryViewData>(deliveryDbo);

            return deliveryViewData;
        }

        public async Task<List<DeliveryViewData>> GetAllDeliveryAsync()
        {
            var deliveriesDbo = await _context.DeliveriesWithoutTracking
                .Include(x => x.User)
                .ToListAsync();

            var deliveriersViewData = _mapper.Map<List<DeliveryViewData>>(deliveriesDbo);

            return deliveriersViewData;
        }


        public async Task UpdateDeliveryAsync(DeliveryViewData model)
        {
            var deliveryDbo = await _context.Deliveries
                .Where(x => x.DeliveryId == model.DeliveryId)
                .FirstOrDefaultAsync();

            if (deliveryDbo is null)
            {
                throw new NotFoundException(EntityNames.Delivery, model.DeliveryId);
            }

            _mapper.Map(model, deliveryDbo);

            await _context.SaveChangesAsync();
        }

        public async Task CreateDeliveryForNewUserAsync(DeliveryViewData model)
        {
            var deliveryDbo = _mapper.Map<DeliveryDbo>(model);

            _context.Deliveries.Add(deliveryDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.Delivery);
            }
        }
    }
}

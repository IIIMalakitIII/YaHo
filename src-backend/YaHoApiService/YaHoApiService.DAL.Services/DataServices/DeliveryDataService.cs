using AutoMapper;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;
using YaHo.YaHoApiService.Common.EntityNames;
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

using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.DeliveryReview;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class DeliveryReviewDataService : IDeliveryReviewDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public DeliveryReviewDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

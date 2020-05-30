using AutoMapper;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.DeliveryReview;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
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

        public async Task AddDeliveryReviewAsync(DeliveryReviewViewData model)
        {
            var deliveryReviewDbo = _mapper.Map<DeliveryReviewDbo>(model);

            _context.DeliveryReviews.Add(deliveryReviewDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.DeliveryReview);
            }
        }
    }
}

using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.CustomerReview;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class CustomerReviewDataService : ICustomerReviewDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public CustomerReviewDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

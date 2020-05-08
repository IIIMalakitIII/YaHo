using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Order;
using YaHo.YaHoApiService.DAL.Services.Context;

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
    }
}

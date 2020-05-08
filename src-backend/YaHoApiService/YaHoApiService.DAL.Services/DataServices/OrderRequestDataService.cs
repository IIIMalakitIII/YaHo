using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.OrderRequest;
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
    }
}

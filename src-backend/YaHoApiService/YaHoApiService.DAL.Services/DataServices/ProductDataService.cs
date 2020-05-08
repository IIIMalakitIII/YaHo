using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class ProductDataService : IProductDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public ProductDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

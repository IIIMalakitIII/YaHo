using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Media;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class MediaDataService : IMediaDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public MediaDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

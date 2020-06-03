using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.Common.Helpers;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Media;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
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

        public async Task AddMediaToProductAsync(List<MediaViewData> model)
        {
            var mediaDbo = _mapper.Map<List<MediaDbo>>(model);

            _context.Media.AddRange(mediaDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.Media);
            }
        }

        public async Task DeleteMediaAsync(int mediaId)
        {
            var mediaDbo = await _context.Media
                .FirstOrDefaultAsync(x => x.MediaId == mediaId);

            ObjectValidationHelper.CheckObjectNotNull(mediaDbo, EntityNames.Media, mediaId);

            _context.Media.Remove(mediaDbo);

            await _context.SaveChangesAsync();
        }
    }
}

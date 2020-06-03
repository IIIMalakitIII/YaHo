using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.MediaViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapMedia()
        {
            #region ViewModel <= => ViewData
            CreateMap<MediaViewModel, MediaViewData>()
                .ReverseMap();
            #endregion

            #region ViewData <= => Dbo
            CreateMap<MediaViewData, MediaDbo>()
                .ReverseMap();
            #endregion 
        }
    }
}

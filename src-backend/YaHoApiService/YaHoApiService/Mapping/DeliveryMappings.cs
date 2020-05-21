using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.DeliveryViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapDeliveries()
        {
            #region ViewModel <= => ViewData
            CreateMap<DeliveryViewModel, DeliveryViewData> ()
                .ForMember(d => d.User,
                    m => m.MapFrom(s => s.UserInfo))
                .ReverseMap();

            #endregion

            #region ViewData <= => Dbo
            CreateMap<DeliveryViewData, DeliveryDbo>()
                .ReverseMap();
            #endregion 
        }
    }
}

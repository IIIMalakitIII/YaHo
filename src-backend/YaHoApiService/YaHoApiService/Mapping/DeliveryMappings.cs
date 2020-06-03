using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            CreateMap<GetDeliveryViewModel, DeliveryViewData>()
                .ReverseMap();

            #endregion

            #region ViewData <= => Dbo

            CreateMap<DeliveryViewData, DeliveryDbo>()
                .ForMember(d => d.OrderRequests,
                    m => m.Ignore())
                .ForMember(d => d.User,
                    m => m.Ignore())
                .ForMember(d => d.DeliveryReviews,
                    m => m.Ignore());


            CreateMap<DeliveryDbo, DeliveryViewData>();

            #endregion
        }
    }
}

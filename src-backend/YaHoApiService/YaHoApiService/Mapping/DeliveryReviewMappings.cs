using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.DeliveryReviewViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapDeliveryReviews()
        {
            #region ViewModel <= => ViewData

            CreateMap<DeliveryReviewViewModel, DeliveryReviewViewData>()
                .ReverseMap();

            CreateMap<LeaveDeliveryReview, DeliveryReviewViewData>()
                .ReverseMap();

            #endregion

            #region ViewData <= => Dbo

            CreateMap<DeliveryReviewViewData, DeliveryReviewDbo>()
                .ReverseMap();

            #endregion 
        }
    }
}

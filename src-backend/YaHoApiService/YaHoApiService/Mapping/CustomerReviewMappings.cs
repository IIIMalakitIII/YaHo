using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.CustomerReviewViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapCustomerReviews()
        {
            #region ViewModel <= => ViewData

            CreateMap<CustomerReviewViewModel, CustomerReviewViewData>()
                .ReverseMap();

            CreateMap<LeaveCustomerReview, CustomerReviewViewData>()
                .ReverseMap();

            #endregion

            #region ViewData <= => Dbo

            CreateMap<CustomerReviewViewData, CustomerReviewDbo>()
                .ReverseMap();

            #endregion 
        }
    }
}

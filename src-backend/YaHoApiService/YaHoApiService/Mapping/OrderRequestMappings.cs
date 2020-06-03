using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.OrderRequestViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapOrderRequests()
        {
            #region ViewModel <= => ViewData
            CreateMap<OrderRequestViewModel, OrderRequestViewData>()
                .ReverseMap();
            #endregion

            #region ViewData <= => Dbo
            CreateMap<OrderRequestViewData, OrderRequestDbo>()
                .ReverseMap();
            #endregion 
        }
    }
}

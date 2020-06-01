using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.LiqPayOrder;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.UserViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public class LiqPayMappings
    {
    }

    public partial class MappingProfile : Profile
    {
        public void MapLiqPay()
        {
            #region ViewModel <= => ViewData
            CreateMap<LiqPayCheckoutFormModel, LiqPayDataViewData>()
                .ReverseMap();
            #endregion

            #region ViewData <= => Dbo
            CreateMap<LiqPayOrderViewData, LiqPayOrderDbo>()
                .ReverseMap();
            #endregion 
        }
    }
}

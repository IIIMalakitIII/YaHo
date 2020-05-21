using AutoMapper;
using Microsoft.CodeAnalysis;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.ProductViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapProducts()
        {
            #region ViewModel <= => ViewData
            CreateMap<ProductViewModel, ProductViewData>()
                .ReverseMap();
            #endregion

            #region ViewData <= => Dbo
            CreateMap<ProductViewData, ProductDbo >()
                .ReverseMap();
            #endregion 
        }
    }
}

using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product.Update;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.ProductViewModels;
using YaHo.YaHoApiService.ViewModels.ProductViewModels.Update;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapProducts()
        {
            #region ViewModel <= => ViewData
            CreateMap<ProductViewModel, ProductViewData>()
                .ReverseMap();

            CreateMap<CreateProductViewModel, ProductViewData>()
                .ReverseMap();

            CreateMap<UpdateProductViewModel, UpdateProductViewData> ()
                .ReverseMap();

            CreateMap<GetProductViewModel, ProductViewData>()
                .ReverseMap();

            #endregion

            #region ViewData <= => Dbo
            CreateMap<ProductViewData, ProductDbo>()
                .ReverseMap();

            CreateMap<UpdateProductViewData, ProductDbo>();

            #endregion
        }
    }
}

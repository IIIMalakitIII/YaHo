using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.CustomerViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapCustomers()
        {
            #region ViewModel <= => ViewData
            CreateMap<CustomerViewModel, CustomerViewData>()
                .ForMember(d => d.User,
                    m => m.MapFrom(s => s.UserInfo))
                .ReverseMap();

            #endregion

            #region ViewData <= => Dbo
            CreateMap<CustomerViewData, CustomerDbo>()
                .ReverseMap();
            #endregion 
        }
    }
}

using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.ViewModels.DeliveryViewModels;
using YaHo.YaHoApiService.ViewModels.UserViewModels;
using YaHo.YaHoApiService.ViewModels.UserViewModels.Auth;
using YaHo.YaHoApiService.ViewModels.UserViewModels.Get;
using YaHo.YaHoApiService.ViewModels.UserViewModels.Update;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapUsers()
        {
            #region ViewModel <= => ViewData
            CreateMap<UserViewModel, UserViewData>()
                .ReverseMap();

            CreateMap<RegisterViewModel, CreateUserViewData>()
                .IncludeBase<UserViewModel, UserViewData>()
                .ReverseMap();


            CreateMap<UpdateUserInfoViewModel, UserViewData>()
                .ReverseMap();

            CreateMap<UpdateUserInfoViewModel, UpdateUserInfoViewData>()
                .ReverseMap();

           /* CreateMap<UserViewData, UpdateUserInfoViewData>()
                .ReverseMap();*/

            CreateMap<GetPartialUserInfoViewModel, UserViewData>()
                .ReverseMap();

            CreateMap<RegisterViewModel, UserViewData>()
                .ForMember(d => d.UserName,
                    m => m.Ignore())
                .ReverseMap();

            CreateMap<GetUserInfoViewModel, UserViewData>()
                .IncludeBase<GetPartialUserInfoViewModel, UserViewData>()
                .ReverseMap();


            #endregion

            #region ViewData <= => Dbo

            CreateMap<UserViewData, UserDbo>()
                .ReverseMap();

            CreateMap<UpdateUserInfoViewData, UserDbo>()
                .IncludeBase<UserViewData, UserDbo>()
                .ForMember(d => d.UserName,
                    m => m.Ignore())
                .ForMember(d => d.Email,
                    m => m.Ignore())
                .ForMember(d => d.Balance,
                    m => m.Ignore())
                .ForMember(d => d.Hold,
                    m => m.Ignore())
                .ForMember(d => d.InitialDate,
                    m => m.Ignore())
                .ReverseMap();

            CreateMap<CreateUserViewData, UserDbo>()
                .IncludeBase<UserViewData, UserDbo>()
                .ReverseMap();

            #endregion 
        }
    }
}

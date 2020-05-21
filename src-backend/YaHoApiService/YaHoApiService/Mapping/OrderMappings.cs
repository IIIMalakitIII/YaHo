using System;
using System.Linq;
using AutoMapper;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order.Update;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Enums;
using YaHo.YaHoApiService.ViewModels.OrderViewModels;
using YaHo.YaHoApiService.ViewModels.OrderViewModels.Update;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapOrders()
        {
            #region ViewModel <= => ViewData
            CreateMap<CreateOrderViewModel, OrderViewData>()
                .ForMember(d => d.DeliveryPlace,
                    m => m.MapFrom(s =>
                        string.IsNullOrEmpty(s.DeliveryСountry) ? null : s.DeliveryСountry +
                                                                         (string.IsNullOrEmpty(s.DeliveryCity) ? null : ", " + s.DeliveryCity +
                                                                                                                        (string.IsNullOrEmpty(s.DeliveryAddress) ? null : ", " + s.DeliveryAddress))))
                .ForMember(d => d.DeliveryFrom,
                    m => m.MapFrom(s =>
                            string.IsNullOrEmpty(s.DeliveryFromСountry) ? null : s.DeliveryFromСountry +
                                                                                 (string.IsNullOrEmpty(s.DeliveryFromCity) ? null : ", " + s.DeliveryFromCity)))
                .ReverseMap();

            CreateMap<UpdateOrderViewModel, UpdateOrderViewData>()
                .ForMember(d => d.DeliveryPlace,
                    m => m.MapFrom(s =>
                        string.IsNullOrEmpty(s.DeliveryСountry) ? null : s.DeliveryСountry +
                                                                         (string.IsNullOrEmpty(s.DeliveryCity) ? null : ", " + s.DeliveryCity +
                                                                                                                        (string.IsNullOrEmpty(s.DeliveryAddress) ? null : ", " + s.DeliveryAddress))))
                .ForMember(d => d.DeliveryFrom,
                    m => m.MapFrom(s =>
                        string.IsNullOrEmpty(s.DeliveryFromСountry) ? null : s.DeliveryFromСountry +
                                                                             (string.IsNullOrEmpty(s.DeliveryFromCity) ? null : ", " + s.DeliveryFromCity)))
                .ReverseMap();

            CreateMap<OrderViewModel, OrderViewData>()
                .ReverseMap();

            CreateMap<OrderUpdateStatusViewModel, OrderUpdateStatusViewData>()
                .ReverseMap();

            CreateMap<OrderFilterViewModel, OrderFilterViewData>()
                .ForMember(d => d.Filter,
                    m => m.MapFrom(s => s.Filter.ToUpper()))
                .ReverseMap();

            CreateMap<OrderStatus, string>()
                .ConvertUsing(x => x.ToString());

            CreateMap<string, OrderStatus>()
                .ConvertUsing(x => Enum.Parse<OrderStatus>(x, true));
            #endregion

            #region ViewData <= => Dbo
            CreateMap<OrderViewData, OrderDbo>()
                .ReverseMap();

            CreateMap<UpdateOrderViewData, OrderDbo>()
                .ForMember(d => d.DeliveryCharge,
                    m => m.Ignore())
                .ForMember(d => d.OrderStatus,
                    m => m.Ignore())
                .ForMember(d => d.ExpectedDateFault,
                    m => m.Ignore())
                .ForMember(d => d.ExpectedDate,
                    m => m.Ignore())
                .ForMember(d => d.DeliveryDate,
                    m => m.Ignore())
                .ReverseMap();
            #endregion
        }
    }
}

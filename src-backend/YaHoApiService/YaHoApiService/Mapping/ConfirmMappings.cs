﻿using AutoMapper;
using System;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Enums;
using YaHo.YaHoApiService.ViewModels.ConfirmViewModels;
using YaHoA.YaHoApiService.ViewModels.ConfirmViewModels;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public void MapConfirms()
        {
            #region ViewModel <= => ViewData
            CreateMap<ConfirmDeliveryChargeViewModel, ConfirmDeliveryChargeViewData>()
                .ReverseMap();

            CreateMap<ConfirmExpectedDateViewModel, ConfirmExpectedDateViewData>()
                .ReverseMap();

            CreateMap<ConfirmOrderStatusViewModel, ConfirmOrderStatusViewData>()
                .ReverseMap();

            CreateMap<CreateConfirmOrderStatusViewModel, CreateConfirmOrderStatusViewData>()
                .ForMember(d => d.NewStatus,
                m => m.MapFrom(s => Enum.Parse<OrderStatus>(s.NewOrderStatus, true)))
                .ForMember(d => d.InitialDate,
                    m => m.Ignore())
                .ForMember(d => d.PreviousStatus,
                    m => m.Ignore())
                .ReverseMap();

            CreateMap<CreateConfirmDeliveryChargeViewModel, CreateConfirmDeliveryChargeViewData>()
                .ForMember(d => d.InitialDate,
                    m => m.Ignore())
                .ForMember(d => d.PreviousPrice,
                    m => m.Ignore())
                .ReverseMap();

            CreateMap<CreateConfirmExpectedDateViewModel, CreateConfirmExpectedDateViewData>()
                .ForMember(d => d.InitialDate,
                    m => m.Ignore())
                .ForMember(d => d.PreviousExpectedDate,
                    m => m.Ignore())
                .ReverseMap();

            #endregion

            #region ViewData <= => Dbo
            CreateMap<CreateConfirmDeliveryChargeViewData, ConfirmDeliveryChargeDbo>()
                .ReverseMap();

            CreateMap<CreateConfirmExpectedDateViewData, ConfirmExpectedDateDbo>()
                .ReverseMap();

            CreateMap<CreateConfirmOrderStatusViewData, ConfirmOrderStatusDbo>()
                .ReverseMap();

            CreateMap<ConfirmDeliveryChargeViewData, ConfirmDeliveryChargeDbo>()
                .ReverseMap();

            CreateMap<ConfirmExpectedDateViewData, ConfirmExpectedDateDbo>()
                .ReverseMap();

            CreateMap<ConfirmOrderStatusViewData, ConfirmOrderStatusDbo>()
                .ReverseMap();
            #endregion 
        }
    }
}

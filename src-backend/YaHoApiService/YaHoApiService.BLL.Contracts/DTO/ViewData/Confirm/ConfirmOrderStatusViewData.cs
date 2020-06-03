using System;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm
{
    public class ConfirmOrderStatusViewData
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public OrderStatus PreviousStatus { get; set; }

        public OrderStatus NewStatus { get; set; }

        public DateTime InitialDate { get; set; }

        public string CreaterId { get; set; }

        public OrderViewData Order { get; set; }
    }
}

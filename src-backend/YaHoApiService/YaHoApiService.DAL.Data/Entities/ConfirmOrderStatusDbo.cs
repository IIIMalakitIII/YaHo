using System;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class ConfirmOrderStatusDbo
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public OrderStatus PreviousStatus { get; set; }

        public OrderStatus NewStatus { get; set; }

        public string CreaterId { get; set; }

        public DateTime InitialDate { get; set; }

        public OrderDbo Order { get; set; }

        public UserDbo User { get; set; }
    }
}

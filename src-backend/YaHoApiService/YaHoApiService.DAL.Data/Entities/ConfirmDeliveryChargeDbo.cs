using System;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class ConfirmDeliveryChargeDbo
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public decimal PreviousPrice { get; set; }

        public decimal NewPrice { get; set; }

        public DateTime InitialDate { get; set; }

        public OrderDbo Order { get; set; }
    }
}

using System;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class ConfirmExpectedDateDbo
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public DateTime PreviousExpectedDate { get; set; }

        public DateTime NewExpectedDate { get; set; }

        public string CreaterId { get; set; }

        public DateTime InitialDate { get; set; }

        public OrderDbo Order { get; set; }

        public UserDbo User { get; set; }
    }
}

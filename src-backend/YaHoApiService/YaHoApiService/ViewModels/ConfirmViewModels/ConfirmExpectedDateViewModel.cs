using System;
using YaHo.YaHoApiService.ViewModels.OrderViewModels;

namespace YaHo.YaHoApiService.ViewModels.ConfirmViewModels
{
    public class ConfirmExpectedDateViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public DateTime PreviousExpectedDate { get; set; }

        public DateTime NewExpectedDate { get; set; }

        public DateTime InitialDate { get; set; }

        public OrderViewModel Orders { get; set; }
    }
}

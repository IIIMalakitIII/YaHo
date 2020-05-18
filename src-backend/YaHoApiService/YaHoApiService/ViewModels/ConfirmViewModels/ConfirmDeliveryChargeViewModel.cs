using System;
using YaHo.YaHoApiService.ViewModels.OrderViewModels;

namespace YaHoA.YaHoApiService.ViewModels.ConfirmViewModels
{
    public class ConfirmDeliveryChargeViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public int PreviousPrice { get; set; }

        public int NewPrice { get; set; }

        public DateTime InitialDate { get; set; }

        public OrderViewModel Orders { get; set; }
    }
}

using System;

namespace YaHo.YaHoApiService.ViewModels.ConfirmViewModels
{
    public class ConfirmOrderStatusViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public string PreviousStatus { get; set; }

        public string NewStatus { get; set; }

        public DateTime InitialDate { get; set; }
    }
}

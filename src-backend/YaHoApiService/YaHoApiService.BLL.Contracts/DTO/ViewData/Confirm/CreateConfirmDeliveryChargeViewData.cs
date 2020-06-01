using System;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm
{
    public class CreateConfirmDeliveryChargeViewData
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public decimal PreviousPrice { get; set; }

        public decimal NewPrice { get; set; }

        public DateTime InitialDate { get; set; }
    }
}

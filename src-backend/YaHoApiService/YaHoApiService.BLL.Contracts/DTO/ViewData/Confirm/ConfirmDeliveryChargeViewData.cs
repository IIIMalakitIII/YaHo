using System;
using System.Collections.Generic;
using System.Text;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm
{
    public class ConfirmDeliveryChargeViewData
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public decimal PreviousPrice { get; set; }

        public decimal NewPrice { get; set; }

        public DateTime InitialDate { get; set; }

        public OrderViewData Order { get; set; }

    }
}

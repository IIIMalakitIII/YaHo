using System;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Confirm
{
    public class ConfirmExpectedDateViewData
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool? CustomerConfirm { get; set; }

        public bool? DeliveryConfirm { get; set; }

        public bool? AutomaticConfirm { get; set; }

        public DateTime PreviousExpectedDate { get; set; }

        public DateTime NewExpectedDate { get; set; }

        public DateTime InitialDate { get; set; }

        public string CreaterId { get; set; }

        public OrderViewData Order { get; set; }

    }
}

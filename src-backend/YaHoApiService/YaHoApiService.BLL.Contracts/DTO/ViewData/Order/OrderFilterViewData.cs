using System;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order
{
    public class OrderFilterViewData
    {
        public string DeliveryСountry { get; set; }

        public string DeliveryCity { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime? ExpectedDateFrom { get; set; }

        public DateTime? ExpectedDateTo { get; set; }

        public string Filter { get; set; }

        public string DeliveryFromСountry { get; set; }

        public string DeliveryFromCity { get; set; }

    }
}

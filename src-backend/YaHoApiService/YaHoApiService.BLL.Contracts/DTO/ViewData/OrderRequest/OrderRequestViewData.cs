using System;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest
{
    public class OrderRequestViewData
    {
        public int OrderRequestId { get; set; }

        public int OrderId { get; set; }

        public int DeliveryId { get; set; }

        public bool? Approved { get; set; }

        public DateTime? InitialDate { get; set; }

        public DeliveryViewData Delivery { get; set; }

    }
}

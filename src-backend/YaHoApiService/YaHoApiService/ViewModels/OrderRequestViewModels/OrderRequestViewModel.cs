using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaHo.YaHoApiService.ViewModels.DeliveryViewModels;

namespace YaHo.YaHoApiService.ViewModels.OrderRequestViewModels
{
    public class OrderRequestViewModel
    {
        public int OrderRequestId { get; set; }

        public int OrderId { get; set; }

        public int DeliveryId { get; set; }

        public bool? Approved { get; set; }

        public DeliveryViewModel Delivery { get; set; }
    }
}

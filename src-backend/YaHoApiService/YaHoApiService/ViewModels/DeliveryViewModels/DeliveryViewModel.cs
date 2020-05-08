using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaHo.YaHoApiService.ViewModels.DeliveryViewModels
{
    public class DeliveryViewModel
    {
        public int DeliveryId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public int? Rating { get; set; }
    }
}

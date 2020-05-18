using System.Collections.Generic;
using YaHo.YaHoApiService.ViewModels.DeliveryReviewViewModels;
using YaHo.YaHoApiService.ViewModels.UserViewModels;

namespace YaHo.YaHoApiService.ViewModels.DeliveryViewModels
{
    public class DeliveryViewModel
    {
        public int DeliveryId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public int? Rating { get; set; }

        public UserViewModel UserInfo { get; set; }

        public List<DeliveryReviewViewModel> DeliveryReviews { get; set; }
    }
}

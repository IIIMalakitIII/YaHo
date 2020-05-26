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

        public double Rating { get; set; }

        public int TotalRating { get; set; }

        public UserViewModel UserInfo { get; set; }

        public IEnumerable<DeliveryReviewViewModel> DeliveryReviews { get; set; }
    }
}

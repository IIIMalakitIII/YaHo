using YaHo.YaHoApiService.ViewModels.UserViewModels;

namespace YaHo.YaHoApiService.ViewModels.DeliveryViewModels
{
    public class DeliveryViewModel
    {
        public int DeliveryId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int TotalReviewCount { get; set; }

        public UserViewModel UserInfo { get; set; }
    }
}

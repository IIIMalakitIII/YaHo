using YaHo.YaHoApiService.ViewModels.DeliveryViewModels;
using YaHo.YaHoApiService.ViewModels.UserViewModels;

namespace YaHo.YaHoApiService.ViewModels.DeliveryReviewViewModels
{
    public class DeliveryReviewViewModel
    {
        public int ReviewId { get; set; }

        public string Description { get; set; }

        public int DeliveryId { get; set; }

        public string UserId { get; set; }

        public int Mark { get; set; }

        public UserViewModel User { get; set; }

        public DeliveryViewModel Delivery { get; set; }
    }
}

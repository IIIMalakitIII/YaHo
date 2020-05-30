using System.Collections.Generic;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery
{
    public class DeliveryViewData
    {
        public int DeliveryId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int TotalRating { get; set; }

        public UserViewData User { get; set; }

        public IEnumerable<DeliveryReviewViewData> DeliveryReviews { get; set; }
    }
}

using System.Collections.Generic;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class DeliveryDbo
    {
        public int DeliveryId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public int? Rating { get; set; }

        public UserDbo User { get; set; }
         
        public ICollection<DeliveryReviewDbo> DeliveryReviews { get; set; }

        public ICollection<OrderRequestDbo> OrderRequests { get; set; }

    }
}

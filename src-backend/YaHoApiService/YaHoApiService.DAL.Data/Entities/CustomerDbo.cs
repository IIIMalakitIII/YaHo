using System.Collections.Generic;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class CustomerDbo
    {
        public int CustomerId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public int? Rating { get; set; }

        public UserDbo User { get; set; }

        public ICollection<CustomerReviewDbo> CustomerReviews { get; set; }

        public ICollection<OrderDbo> Orders { get; set; }

    }
}

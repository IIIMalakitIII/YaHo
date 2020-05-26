using System.Collections.Generic;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer
{
    public class CustomerViewData
    {
        public int CustomerId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public int? Rating { get; set; }

        public int? TotalRating { get; set; }

        public UserViewData User { get; set; }

        public IEnumerable<CustomerReviewViewData> CustomerReviews { get; set; }

        // public IEnumerable<OrderDbo> Orders { get; set; }
    }
}

using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview
{
    public class CustomerReviewViewData
    {
        public int ReviewId { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public int CustomerId { get; set; }

        public int? Mark { get; set; }

        public UserViewData User { get; set; }

        public CustomerViewData Customer { get; set; }
    }
}

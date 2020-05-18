using System.Collections.Generic;
using YaHo.YaHoApiService.ViewModels.CustomerReviewViewModels;
using YaHo.YaHoApiService.ViewModels.UserViewModels;

namespace YaHo.YaHoApiService.ViewModels.CustomerViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public int? Rating { get; set; }
        
        public UserViewModel UserInfo { get; set; }
        
        public List<CustomerReviewViewModel> CustomerReviews { get; set; }
    }
}

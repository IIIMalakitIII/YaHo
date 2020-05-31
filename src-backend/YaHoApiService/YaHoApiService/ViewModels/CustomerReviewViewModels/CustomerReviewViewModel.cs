using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YaHo.YaHoApiService.ViewModels.UserViewModels;

namespace YaHo.YaHoApiService.ViewModels.CustomerReviewViewModels
{
    public class CustomerReviewViewModel
    {
        public int ReviewId { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public int CustomerId { get; set; }

        public int? Mark { get; set; }

        public UserViewModel User { get; set; }

    }
}

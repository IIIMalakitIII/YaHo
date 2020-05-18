using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.CustomerReviewViewModels
{
    public class CustomerReviewViewModel
    {
        [Required]
        public int ReviewId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public int? Mark { get; set; }

    }
}

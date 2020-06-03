using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YaHo.YaHoApiService.ViewModels.CustomerReviewViewModels
{
    public class LeaveCustomerReview
    {
        public string Description { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int Mark { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.DeliveryReviewViewModels
{
    public class LeaveDeliveryReview
    {
        public string Description { get; set; }

        [Required]
        public int DeliveryId { get; set; }

        [Required]
        public int Mark { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace YaHoA.YaHoApiService.ViewModels.ConfirmViewModels
{
    public class CreateConfirmDeliveryChargeViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int NewPrice { get; set; }

    }
}

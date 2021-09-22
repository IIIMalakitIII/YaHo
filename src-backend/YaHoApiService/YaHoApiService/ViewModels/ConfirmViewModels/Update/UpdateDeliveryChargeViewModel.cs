using System.ComponentModel.DataAnnotations;

namespace YaHoApiService.ViewModels.ConfirmViewModels.Update
{
    public class UpdateDeliveryChargeViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool DeliveryConfirm { get; set; }
    }
}

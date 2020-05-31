using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.ConfirmViewModels
{
    public class CreateConfirmExpectedDateViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int NewExpectedDate { get; set; }
    }
}

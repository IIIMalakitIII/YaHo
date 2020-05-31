using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.ConfirmViewModels.Update
{
    public class UpdateExpectedDateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool UserConfirm { get; set; }
    }
}

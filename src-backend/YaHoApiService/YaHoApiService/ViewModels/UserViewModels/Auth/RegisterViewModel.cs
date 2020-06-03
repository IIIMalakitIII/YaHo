using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.UserViewModels.Auth
{
    public class RegisterViewModel : UserViewModel
    {
        [Required]
        public string Password { get; set; }

    }
}

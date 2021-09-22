
using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.UserViewModels.Auth
{
    public class LogInViewModel
    {

        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}

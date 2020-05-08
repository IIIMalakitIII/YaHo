using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Description { get; set; }
    }
}

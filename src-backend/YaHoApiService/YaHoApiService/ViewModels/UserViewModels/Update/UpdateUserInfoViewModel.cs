using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.UserViewModels.Update
{
    public class UpdateUserInfoViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Description { get; set; }
    }
}

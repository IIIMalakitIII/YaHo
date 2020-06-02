using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.ConfirmViewModels
{
    public class CreateConfirmOrderStatusViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string NewOrderStatus { get; set; }
    }
}

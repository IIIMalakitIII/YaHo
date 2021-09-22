using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.OrderViewModels.Update
{
    public class OrderUpdateStatusViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string OrderStatus { get; set; }
    }
}

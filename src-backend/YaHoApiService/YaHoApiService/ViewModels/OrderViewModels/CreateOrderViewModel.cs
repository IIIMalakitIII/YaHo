using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.OrderViewModels
{
    public class CreateOrderViewModel
    {
        public int OrderId { get; set; }

        [Required]
        public string DeliveryСountry { get; set; }

        [Required]
        public string DeliveryCity { get; set; }

        [Required]
        public int DeliveryCharge { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        public bool Bargain { get; set; }

        [Required]
        public string ExpectedDate { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public string DeliveryFromСountry { get; set; }

        [Required]
        public string DeliveryFromCity { get; set; }


    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YaHo.YaHoApiService.ViewModels.MediaViewModels;

namespace YaHo.YaHoApiService.ViewModels.ProductViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        public string Description { get; set; }

        public string Link { get; set; }

        [Required]
        public string ProductName { get; set; }
    }
}

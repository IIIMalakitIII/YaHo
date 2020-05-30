using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.ViewModels.ProductViewModels.Update
{
    public class UpdateProductViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Description { get; set; }

        public string Link { get; set; }

        [Required]
        public string ProductName { get; set; }
    }
}

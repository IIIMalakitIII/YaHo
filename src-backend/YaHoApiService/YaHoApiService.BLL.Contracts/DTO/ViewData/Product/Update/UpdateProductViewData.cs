using System.ComponentModel.DataAnnotations;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product.Update
{
    public class UpdateProductViewData
    {
        public int ProductId { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string ProductName { get; set; }
    }
}

using System.Collections.Generic;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product
{
    public class ProductViewData
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Price { get; set; }

        public int Tax { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string ProductName { get; set; }

        public OrderViewData Order { get; set; }

        public IEnumerable<MediaViewData> Media { get; set; }
    }
}

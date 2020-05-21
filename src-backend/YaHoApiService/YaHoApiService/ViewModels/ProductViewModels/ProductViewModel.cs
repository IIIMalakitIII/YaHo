using System.Collections.Generic;
using YaHo.YaHoApiService.ViewModels.MediaViewModels;

namespace YaHo.YaHoApiService.ViewModels.ProductViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Price { get; set; }

        public int Tax { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string ProductName { get; set; }

        public IEnumerable<MediaViewModel> Media { get; set; }
    }
}

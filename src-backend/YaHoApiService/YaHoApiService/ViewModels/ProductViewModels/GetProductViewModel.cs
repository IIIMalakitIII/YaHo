using System.Collections.Generic;
using YaHo.YaHoApiService.ViewModels.MediaViewModels;

namespace YaHo.YaHoApiService.ViewModels.ProductViewModels
{
    public class GetProductViewModel: ProductViewModel
    {
        public IEnumerable<MediaViewModel> Media { get; set; }
    }
}

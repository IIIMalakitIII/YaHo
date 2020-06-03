using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Media
{
    public interface IMediaService
    {
        Task AddMediaToProduct(List<MediaViewData> model, int productId, int customerId);

        Task DeleteMedia(int mediaId, int productId, int customerId);
    }
}

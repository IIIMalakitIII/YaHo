using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Media
{
    public interface IMediaDataService
    {
        Task DeleteMediaAsync(int mediaId);

        Task AddMediaToProductAsync(List<MediaViewData> model);
    }
}

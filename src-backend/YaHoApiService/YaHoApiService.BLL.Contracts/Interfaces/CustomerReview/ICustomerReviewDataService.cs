using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.CustomerReview
{
    public interface ICustomerReviewDataService
    {
        Task AddCustomerReviewAsync(CustomerReviewViewData model);
    }
}

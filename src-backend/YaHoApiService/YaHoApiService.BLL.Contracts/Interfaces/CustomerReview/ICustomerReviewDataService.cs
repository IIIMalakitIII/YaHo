using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.CustomerReview
{
    public interface ICustomerReviewDataService
    {
        Task AddCustomerReviewAsync(CustomerReviewViewData model);

        Task<double> CalculateCustomerRating(int customerId);

        Task<List<CustomerReviewViewData>> GetCustomerReviewAsync(int customerId);
    }
}

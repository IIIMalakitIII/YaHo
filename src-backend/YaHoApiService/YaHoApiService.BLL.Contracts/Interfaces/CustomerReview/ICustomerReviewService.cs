using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.CustomerReview
{
    public interface ICustomerReviewService
    {
        Task<List<CustomerReviewViewData>> GetCustomerReviews(int customerId);

        Task AddCustomerReview(CustomerReviewViewData model, int userCustomerId);
    }
}

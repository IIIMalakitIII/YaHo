using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.DeliveryReview
{
    public interface IDeliveryReviewService
    {
        Task AddDeliveryReview(DeliveryReviewViewData model, int userDeliveryId);

        Task<List<DeliveryReviewViewData>> GetDeliveryReviews(int deliveryId);
    }
}

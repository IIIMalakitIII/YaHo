using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.DeliveryReview
{
    public interface IDeliveryReviewDataService
    {
        Task AddDeliveryReviewAsync(DeliveryReviewViewData model);

        Task<List<DeliveryReviewViewData>> GetDeliveryReviewAsync(int deliveryId);

        Task<double> CalculateDeliveryRating(int deliveryId);
    }
}

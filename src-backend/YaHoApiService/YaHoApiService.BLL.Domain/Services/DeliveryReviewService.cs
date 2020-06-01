using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.DeliveryReview;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview;
using YaHo.YaHoApiService.BLL.Domain.Validations;
using YaHo.YaHoApiService.Common.Exceptions;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class DeliveryReviewService : IDeliveryReviewService
    {
        private readonly IDeliveryReviewDataService _deliveryReviewDataService;

        private readonly IDeliveryDataService _deliveryDataService;

        private readonly UserValidator _userValidator;

        private readonly DeliveryValidator _deliveryValidator;

        public DeliveryReviewService(IDeliveryReviewDataService deliveryReviewDataService,
            IDeliveryDataService deliveryDataService,
            IUserDataService userDataService)
        {
            _deliveryReviewDataService = deliveryReviewDataService;
            _deliveryDataService = deliveryDataService;
            _userValidator = new UserValidator(userDataService);
            _deliveryValidator = new DeliveryValidator(deliveryDataService);
        }

        public async Task AddDeliveryReview(DeliveryReviewViewData model, int userDeliveryId)
        {
            if (model.DeliveryId == userDeliveryId)
            {
                throw new BusinessLogicException("You can’t leave a review for yourself!");
            }
            await _deliveryValidator.CheckDeliveryWithThisIdExists(model.DeliveryId);
            await _userValidator.CheckUserWithThisIdExists(model.UserId);

            await _deliveryReviewDataService.AddDeliveryReviewAsync(model);
            await UpdateDeliveryRating(model.DeliveryId, model.Mark);
        }

        public async Task<List<DeliveryReviewViewData>> GetDeliveryReviews(int deliveryId)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);

            var deliveryReviewsViewData = await _deliveryReviewDataService.GetDeliveryReviewAsync(deliveryId);

            return deliveryReviewsViewData;
        }


        #region Private_method

        private async Task UpdateDeliveryRating(int deliveryId, int newMark)
        {
            var delivery = await _deliveryDataService.GetDeliveryWithoutIncludeAsync(deliveryId);
            delivery.Rating = await _deliveryReviewDataService.CalculateDeliveryRating(deliveryId);
            delivery.TotalReviewCount += 1;

            await _deliveryDataService.UpdateDeliveryAsync(delivery);
        }

        #endregion
    }
}

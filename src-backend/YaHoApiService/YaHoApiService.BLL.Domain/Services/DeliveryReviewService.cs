using System;
using AutoMapper;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.DeliveryReview;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview;
using YaHo.YaHoApiService.BLL.Domain.Validations;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class DeliveryReviewService : IDeliveryReviewService
    {
        private readonly IDeliveryReviewDataService _deliveryReviewDataService;

        private readonly IDeliveryDataService _deliveryDataService;

        private readonly IUserDataService _userDataService;

        private readonly IMapper _mapper;

        private readonly UserValidator _userValidator;

        private readonly DeliveryValidator _deliveryValidator;

        public DeliveryReviewService(IDeliveryReviewDataService deliveryReviewDataService,
            IDeliveryDataService deliveryDataService,
            IUserDataService userDataService, IMapper mapper)
        {
            _deliveryReviewDataService = deliveryReviewDataService;
            _userDataService = userDataService;
            _deliveryDataService = deliveryDataService;
            _mapper = mapper;
            _userValidator = new UserValidator(userDataService);
            _deliveryValidator = new DeliveryValidator(deliveryDataService);
        }

        public async Task AddDeliveryReview(DeliveryReviewViewData model)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(model.DeliveryId);
            await _userValidator.CheckUserWithThisIdExists(model.UserId);

            await _deliveryReviewDataService.AddDeliveryReviewAsync(model);
            await UpdateDeliveryRating(model.DeliveryId, model.Mark);
        }


        #region Private_method

        private async Task UpdateDeliveryRating(int deliveryId, int newMark)
        {
            var delivery = await _deliveryDataService.GetDeliveryWithoutIncludeAsync(deliveryId);

            var rating = (delivery.Rating * delivery.TotalRating + newMark) / (delivery.TotalRating + 1);

            delivery.Rating = Math.Round(rating, 1);
            delivery.TotalRating += newMark;

            await _deliveryDataService.UpdateDeliveryAsync(delivery);
        }

        #endregion
    }
}

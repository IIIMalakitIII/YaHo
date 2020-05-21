using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;
using YaHo.YaHoApiService.BLL.Domain.Validations;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryDataService _deliveryDataService;

        private readonly IUserDataService _userDataService;

        private readonly IMapper _mapper;

        private readonly DeliveryValidator _deliveryValidator;

        private readonly UserValidator _userValidator;

        public DeliveryService(IDeliveryDataService deliveryDataService,
            IUserDataService userDataService, IMapper mapper)
        {
            _mapper = mapper;
            _deliveryDataService = deliveryDataService;
            _userDataService = userDataService;
            _userValidator = new UserValidator(_userDataService);
            _deliveryValidator = new DeliveryValidator(deliveryDataService);
        }

        public async Task UpdateDeliveryDescription(int deliveryId, string description)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);

            var delivery = await _deliveryDataService.GetDeliveryWithoutIncludeAsync(deliveryId);
            delivery.Description = description;

            await _deliveryDataService.UpdateDeliveryAsync(delivery);
        }

        public async Task<DeliveryViewData> GetDelivery(int deliveryId)
        {
            await _deliveryValidator.CheckDeliveryWithThisIdExists(deliveryId);

            var delivery = await _deliveryDataService.GetDeliveryAsync(deliveryId);

            return delivery;
        }

        public async Task<DeliveryViewData> GetDeliveryInfoByUserId(string userId)
        {
            await _userValidator.CheckUserWithThisIdExists(userId);

            var customer = await _deliveryDataService.GetCustomerByUserIdAsync(userId);

            return customer;
        }

        public async Task<List<DeliveryViewData>> GetAllDelivery()
        {
            var delivery = await _deliveryDataService.GetAllDeliveryAsync();

            return delivery;
        }
    }
}

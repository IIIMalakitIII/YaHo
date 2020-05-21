using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;

namespace YaHo.YaHoApiService.BLL.Domain.Validations
{
    internal class DeliveryValidator
    {
        private readonly IDeliveryDataService _deliveryDataService;

        public DeliveryValidator(IDeliveryDataService deliveryDataService)
        {
            _deliveryDataService = deliveryDataService;
        }


        public async Task CheckDeliveryWithThisIdExists(int id)
        {
            if (!await _deliveryDataService.IsDeliveryWithIdExistsAsync(id))
            {
                throw new ValidationException($"No delivery with this id: '{id}'.");
            }
        }
    }
}

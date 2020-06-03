using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;

namespace YaHo.YaHoApiService.BLL.Domain.Validations
{
    internal class CustomerValidator
    {
        private readonly ICustomerDataService _customerDataService;

        public CustomerValidator(ICustomerDataService customerDataService)
        {
            _customerDataService = customerDataService;
        }


        public async Task CheckCustomerWithThisIdExists(int id)
        {
            if (!await _customerDataService.IsCustomerWithIdExistsAsync(id))
            {
                throw new ValidationException($"No customer with this id: '{id}'.");
            }
        }

    }
}

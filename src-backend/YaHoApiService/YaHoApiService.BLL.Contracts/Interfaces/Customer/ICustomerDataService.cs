using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer
{
    public interface ICustomerDataService
    {
        Task CreateCustomerForNewUserAsync(CustomerViewData model);

        Task<bool> IsCustomerWithIdExistsAsync(int id);

        Task<CustomerViewData> GetCustomerAsync(int id);

        Task UpdateCustomerAsync(CustomerViewData model);

        Task<List<CustomerViewData>> GetAllCustomerAsync();
    }
}

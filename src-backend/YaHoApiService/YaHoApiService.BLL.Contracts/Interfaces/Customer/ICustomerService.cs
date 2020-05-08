using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer
{
    public interface ICustomerService
    {
        Task UpdateCustomerDescription(int customerId, string description);

        Task<CustomerViewData> GetCustomer(int customerId);

        Task<List<CustomerViewData>> GetAllCustomer();
    }
}

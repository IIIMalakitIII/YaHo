using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;
using YaHo.YaHoApiService.BLL.Domain.Validations;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDataService _customerDataService;

        private readonly IMapper _mapper;

        private readonly CustomerValidator _customerValidator;

        public CustomerService(ICustomerDataService customerDataService, IMapper mapper)
        {
            _mapper = mapper;
            _customerDataService = customerDataService;
            _customerValidator = new CustomerValidator(customerDataService);
        }

        public async Task UpdateCustomerDescription(int customerId, string description)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);

            var customer = await _customerDataService.GetCustomerAsync(customerId);
            customer.Description = description;

            await _customerDataService.UpdateCustomerAsync(customer);
        }

        public async Task<CustomerViewData> GetCustomer(int customerId)
        {
            await _customerValidator.CheckCustomerWithThisIdExists(customerId);

            var customer = await _customerDataService.GetCustomerAsync(customerId);

            return customer;
        }

        public async Task<List<CustomerViewData>> GetAllCustomer()
        {
            var customer = await _customerDataService.GetAllCustomerAsync();

            return customer;
        }
    }
}

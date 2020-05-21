using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public CustomerDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> IsCustomerWithIdExistsAsync(int id)
        {
            return await _context.CustomersWithoutTracking.AnyAsync(x => x.CustomerId == id);
        }


        public async Task CreateCustomerForNewUserAsync(CustomerViewData model)
        {
            var customerDbo = _mapper.Map<CustomerDbo>(model);

            _context.Customers.Add(customerDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.Customer);
            }
        }

        public async Task<CustomerViewData> GetCustomerByUserIdAsync(string id)
        {
            var customerDbo = await _context.CustomersWithoutTracking
                .Include(x => x.User)
                .Include(x => x.CustomerReviews)
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();

            var customerViewData = _mapper.Map<CustomerViewData>(customerDbo);

            return customerViewData;
        }

        public async Task<CustomerViewData> GetCustomerAsync(int id)
        {
            var customerDbo = await _context.CustomersWithoutTracking
                .Include(x => x.User)
                .Include(x => x.CustomerReviews)
                .Where(x => x.CustomerId == id)
                .FirstOrDefaultAsync();

            var customerViewData = _mapper.Map<CustomerViewData>(customerDbo);

            return customerViewData;
        }

        public async Task<CustomerViewData> GetCustomerWithoutIncludeAsync(int id)
        {
            var customerDbo = await _context.CustomersWithoutTracking
                .Where(x => x.CustomerId == id)
                .FirstOrDefaultAsync();

            var customerViewData = _mapper.Map<CustomerViewData>(customerDbo);

            return customerViewData;
        }

        public async Task<List<CustomerViewData>> GetAllCustomerAsync()
        {
            var customersDbo = await _context.CustomersWithoutTracking
                .Include(x => x.User)
                .Include(x => x.CustomerReviews)
                .ToListAsync();

            var customersViewData = _mapper.Map<List<CustomerViewData>>(customersDbo);

            return customersViewData;
        }


        public async Task UpdateCustomerAsync(CustomerViewData model)
        {
            var customerDbo = await _context.Customers
                .FirstOrDefaultAsync(x => x.CustomerId == model.CustomerId);

            if (customerDbo is null)
            {
                throw new NotFoundException(EntityNames.Customer, model.CustomerId);
            }

            _mapper.Map(model, customerDbo);

            await _context.SaveChangesAsync();
        }

    }
}

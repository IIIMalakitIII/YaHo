using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.ViewModels.CustomerViewModels;
using YaHo.YaHoApiService.ViewModels.CustomerViewModels.Update;

namespace YaHoApiService.Controller
{
    [Route("api/Customers")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(IMapper mapper, ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer(UpdateCustomerInfoViewModel model)
        {
            await _customerService.UpdateCustomerDescription(model.CustomerId, model.Description);

            return Ok();
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerViewModel>> GetCustomer(int customerId)
        {
            var customerViewData = await _customerService.GetCustomer(customerId);

            var customerViewModel = _mapper.Map<CustomerViewModel>(customerViewData);

            return Ok(customerViewModel);
        }

        [HttpGet("AllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomer()
        {
            var customersViewData = await _customerService.GetAllCustomer();

            var customersViewModel = _mapper.Map<IEnumerable<CustomerViewModel>>(customersViewData);

            return Ok(customersViewModel);
        }
    }
}
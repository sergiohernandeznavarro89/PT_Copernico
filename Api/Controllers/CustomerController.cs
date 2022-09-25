using Api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Helpers;
using Service.Service.IService;

namespace Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> Get(int? page)
        {
            try 
            { 
                PagedList.IPagedList<CustomerDTO> response = await _customerService.GetAll(page);
                return Ok(ApiPaginationResponse<CustomerDTO>.formatResponse(response));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("api/[controller]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                CustomerDTO response = await _customerService.GetById(id);
                return Ok(response);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/[controller]")]
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Add(CustomerDTO customer)
        {
            try 
            { 
                var response = await _customerService.Add(customer);
                customer.id = response;
                return Ok(customer);            
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/[controller]")]
        [HttpPut]
        public async Task<ActionResult<CustomerDTO>> Update(CustomerDTO customer)
        {
            try
            {
                CustomerDTO response = await _customerService.Update(customer);
                return Ok(response);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/[controller]/{id}")]
        [HttpDelete]
        public async Task<ActionResult<int>> Delete(int id)
        {
            try
            {
                await _customerService.Delete(id);
                return Ok(id);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/[controller]/AutoPost")]
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> AutoAdd()
        {
            await _customerService.AutoAdd();
            return Ok();
        }
    }
}

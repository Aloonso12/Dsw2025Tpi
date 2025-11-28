using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerManagementService _serv;
        public CustomersController(CustomerManagementService serv)
        {
            _serv = serv;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequest dto)
        {
            try
            {
                var resp = await _serv.AddCustomer(dto);
                return Created(string.Empty, resp);
            }
            catch (ArgumentException ae) { return BadRequest(ae.Message); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _serv.GetAllCustomers());
        }
    }

}

using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderManagementService _serv;
        public OrdersController(OrderManagementService serv)
        {
            _serv = serv;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest dto)
        {
            try
            {
                var resp = await _serv.CreateOrder(dto);
                return Created("", resp);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                var detail = ex.InnerException?.Message ?? ex.Message;

                return Problem(
                    title: "Error interno al crear la orden",
                    detail: detail
                );
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _serv.GetAllOrders());
        }
    }
}

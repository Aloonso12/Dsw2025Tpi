using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductsManagementService _servi;

    public ProductsController(ProductsManagementService servi)
    {
        _servi = servi;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
    {
        var products = await _servi.GetProducts();
        var list = products!
            .Select(p => new ProductResponse(p.Id, p.Sku,p.InternalCode, p.Name, p.Description,
                                             p.currentUnitPrice, p.stockQuantity, p.IsActive));
        if (!list.Any())
        {
            return NoContent();
        }

        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var p = await _servi.GetProductById(id);
        if (p is null) return NotFound();
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductRequest request)
    {
        try
        {
            var product = await _servi.AddProduct(request);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (DuplicatedEntityException de)
        {
            return Conflict(de.Message);
        }
        catch (Exception)
        {
            return Problem("Se produjo un error al guardar el producto");
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest request)
    {
        try
        {
            var response = await _servi.UpdateProduct(id, request);
            return Ok(response);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (KeyNotFoundException ke)
        {
            return NotFound(ke.Message);
        }
        catch (Exception)
        {
            return Problem("Se produjo un error al actualizar el producto");
        }
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult> Disable(Guid id)
    {
        var p = await _servi.GetProductById(id);
        if (p is null) return NotFound();
        p.Disable();
        await _servi.Elimi(p);
        return NoContent();
    }
}

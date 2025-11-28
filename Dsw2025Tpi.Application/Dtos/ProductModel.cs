
namespace Dsw2025Tpi.Application.Dtos
{
    public record ProductResponse(
         Guid Id,
         string Sku,
         string InternalCode,
         string Name,
         string Description,
         decimal CurrentUnitPrice,
         int stockQuantity,
         bool IsActive
     );
    public record ProductRequest(
        string Sku,
        string InternalCode,
        string Name,
        string Description,
        decimal CurrentUnitPrice,
        int stockQuantity
    );
}


namespace Dsw2025Tpi.Application.Dtos
{
    
    public record ProductRequest(
        string Sku,
        string InternalCode,
        string Name,
        string? Description,
        decimal CurrentUnitPrice,
        int stockQuantity
    );
    public record ProductResponse(
         Guid Id,
         string? Sku,
         string? InternalCode,
         string? Name,
         string? Description,
         decimal? CurrentUnitPrice,
         int? stockQuantity,
         bool IsActive
     );
    public record ResponsePagination(
        List<ProductResponse> ProductItem,
        int Total
    );
    public record FilterProduct(
        string? Status,
        string? Search,
        int? PageNumber,
        int? PageSize
    );
}

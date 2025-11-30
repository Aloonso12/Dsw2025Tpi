using System.Reflection;
using System.Windows.Markup;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dsw2025Tpi.Application.Services
{

    public class ProductsManagementService
    {
        private readonly IRepository _repo;
        private readonly ILogger<ProductsManagementService> _logger;
        public ProductsManagementService(IRepository repo, ILogger<ProductsManagementService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        //Ferchoooo
        public async Task<IEnumerable<Product>?> GetProducts()
        {
            return await _repo.GetAll<Product>();
        }

        public async Task<Product?> GetProductById(Guid id)
        {
            return await _repo.GetById<Product>(id);
        }

        public async Task<ProductResponse> AddProduct(ProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Sku) ||
                string.IsNullOrWhiteSpace(request.Name) ||
                request.CurrentUnitPrice < 0)
            {
                throw new ArgumentException("Valores para el producto no válidos");
            }

            var existing = await _repo.First<Product>(p => p.Sku == request.Sku);
            if (existing != null)
            {
                throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");
            }

            var p = new Product(request.Sku,request.InternalCode, request.Name, request.Description, request.CurrentUnitPrice, request.stockQuantity);
            await _repo.Add<Product>(p);

            return new ProductResponse(p.Id,
                p.Sku,
                p.InternalCode,
                p.Name,
                p.Description,
                p.currentUnitPrice,
                p.stockQuantity,
                p.IsActive);
        }
        public async Task<ProductResponse> UpdateProduct(Guid id, ProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Sku) ||
                string.IsNullOrWhiteSpace(request.Name) ||
                request.CurrentUnitPrice < 0)
            {
                throw new ArgumentException("Valores para el producto no válidos");
            }

            var p = await _repo.GetById<Product>(id);
            if (p == null)
                throw new KeyNotFoundException($"Producto con Id '{id}' no encontrado.");

            p.ChangeName(request.Name);
            p.ChangePrice(request.CurrentUnitPrice);
            p.AdjustStock(request.stockQuantity - p.stockQuantity);
            p.ChangeDescription(request.Description);

            await _repo.Update<Product>(p);

            return new ProductResponse(
                p.Id,
                p.Sku,
                p.InternalCode,
                p.Name,
                p.Description,
                p.currentUnitPrice,
                p.stockQuantity,
                p.IsActive
            );
        }
        public async Task Elimi(Product p)
        {
            p.Disable();
            await _repo.Update<Product>(p);
        }
        public async Task<ResponsePagination?> GetProducts(FilterProduct request)
        {
            var isActive = request.Status == "enabled" 
            ? (bool?)true 
            : request.Status == "disabled" 
                ? (bool?)false
                : null;

            _logger.LogInformation("Consulta de Productos");

            var activeProducts = await _repo.GetFiltered<Product>(p => (
                isActive == null || p.IsActive == isActive) 
                && (string.IsNullOrEmpty(request.Search) || p.Name.Contains(request.Search))
            );
            if (activeProducts is null || !activeProducts.Any())
            throw new Exception("No products were found");

            var products = activeProducts.Select(p => new ProductResponse(
                p.Id,
                p.Sku,
                p.InternalCode,
                p.Name,
                p.Description,
                p.currentUnitPrice,
                p.stockQuantity,
                p.IsActive))
                .OrderBy(p => p.Sku)
                .Skip((request.PageNumber -1) * request.PageSize ?? 0)
                .Take(request.PageSize ?? activeProducts.Count());

            return new ResponsePagination(products.ToList(), activeProducts.Count());
        }
    }
}

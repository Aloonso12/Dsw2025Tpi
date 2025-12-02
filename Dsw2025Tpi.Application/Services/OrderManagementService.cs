using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Services
{
    public class OrderManagementService
    {
        private readonly IRepository _repo;
        public OrderManagementService(IRepository repo) => _repo = repo;

        public async Task<OrderResponse> CreateOrder(CreateOrderRequest dto)
        {
            var user = await _repo.GetById<User>(dto.UserId)
                ?? throw new ArgumentException("Cliente no encontrado");

            var order = new Order(dto.UserId, dto.ShippingAddress, dto.BillingAddress);

            foreach (var itemDto in dto.OrderItems)
            {
                var product = await _repo.GetById<Product>(itemDto.ProductId)
                    ?? throw new ArgumentException($"Producto {itemDto.ProductId} no existe");

                if (product.stockQuantity < itemDto.Quantity)
                    throw new InvalidOperationException($"no hay suficiente {product.Name}");

                order.AddItem(new OrderItem(
                  product.Id,
                  itemDto.Quantity,
                  product.currentUnitPrice
                ));
                product.AdjustStock(-itemDto.Quantity);
                await _repo.Update<Product>(product);
            }

            await _repo.Add<Order>(order);
            return Map(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrders()
        {
            var orders = await _repo.GetAll<Order>("Items");
            return orders.Select(Map);
        }

        public async Task<OrderResponse> UpdateOrderStatus(Guid orderId, UpdateOrderStatusRequest dto)
        {
            var order = await _repo.GetById<Order>(orderId, "Items")
                ?? throw new ArgumentException("Orden no encontrada");

            order.UpdateStatus(dto.Status);
            var updatedOrder = await _repo.Update<Order>(order);
            
            return Map(updatedOrder);
        }

        private static OrderResponse Map(Order o) => new(
            o.Id,
            o.UserId,
            o.Date,
            o.ShippingAddress,
            o.BillingAddress,
            o.Notes,
            o.Status,
            o.TotalAmount,
            o.Items.Select(i => new OrderItemResponse(
                i.ProductId,
                i.Quantity,
                i.UnitPrice,
                i.Subtotal
            ))
        );
    }
}

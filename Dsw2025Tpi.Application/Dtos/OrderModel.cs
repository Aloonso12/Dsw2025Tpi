using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Application.Dtos
{
    public record OrderItemResponse(Guid ProductId, int Quantity, decimal UnitPrice, decimal Subtotal);
    public record OrderResponse(Guid Id, Guid UserId, DateTime Date, string ShippingAddress, string BillingAddress, string Notes, OrderStatus Status, decimal TotalAmount, IEnumerable<OrderItemResponse> Items);
    public record CreateOrderRequest(Guid UserId, string ShippingAddress, string BillingAddress, List<CreateOrderItemRequest> OrderItems);
    public record CreateOrderItemRequest(Guid ProductId, int Quantity);
    public record UpdateOrderStatusRequest(OrderStatus Status);
}

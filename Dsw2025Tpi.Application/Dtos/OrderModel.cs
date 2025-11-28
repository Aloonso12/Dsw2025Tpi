using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos
{
    public record OrderItemResponse(Guid ProductId, int Quantity, decimal UnitPrice, decimal Subtotal);
    public record OrderResponse(Guid Id, Guid CustomerId, DateTime Date, string ShippingAddress, string BillingAddress, string Notes, decimal TotalAmount, IEnumerable<OrderItemResponse> Items);
    public record CreateOrderRequest(Guid CustomerId, string ShippingAddress, string BillingAddress, List<CreateOrderItemRequest> OrderItems);
    public record CreateOrderItemRequest(Guid ProductId, int Quantity);
}

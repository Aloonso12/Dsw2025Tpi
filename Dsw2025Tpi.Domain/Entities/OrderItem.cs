using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public Order Order { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Subtotal => Quantity * UnitPrice;

        private OrderItem() {}

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0) throw new ArgumentException("La cantidad tiene que ser mayor a 0", nameof(quantity));
            if (unitPrice < 0) throw new ArgumentException("El precio no debe ser negativo", nameof(unitPrice));

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
        public void AttachToOrder(Order order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
            OrderId = order.Id;
        }
    }
}

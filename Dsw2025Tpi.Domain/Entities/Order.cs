using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Order : EntityBase
    {
        public Guid UserId { get; private set; }
        public DateTime Date { get; private set; }
        public string ShippingAddress { get; private set; }
        public string BillingAddress { get; private set; }
        public string Notes { get; private set; }
        public OrderStatus Status { get; set; }


        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public decimal TotalAmount => Items.Sum(i => i.Subtotal);

        private Order() {}

        public Order(Guid userId, string shippingAddress, string billingAddress, string notes = "")
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Debes ingresar el cliente", nameof(userId));
            if (string.IsNullOrWhiteSpace(shippingAddress))
                throw new ArgumentException("Shipping address es necesaria", nameof(shippingAddress));
            if (string.IsNullOrWhiteSpace(billingAddress))
                throw new ArgumentException("Billing address es necesaria", nameof(billingAddress));

            Id = Guid.NewGuid();
            UserId = userId;
            Date = DateTime.UtcNow;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Notes = notes;
            Status = OrderStatus.Pending;
        }
        public void AddItem(OrderItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            item.AttachToOrder(this);
            _items.Add(item);
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Sku { get; private set; }
        public string InternalCode { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal currentUnitPrice { get; private set; }
        public int stockQuantity { get; private set; }
        public bool IsActive { get; private set; }

        private Product() { }
        public Product(string sku, string internalCode, string name, string description, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("El SKU es obligatorio", nameof(sku));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es obligatorio", nameof(name));
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "El precio debe ser mayor a 0");
            if (stock < 0)
                throw new ArgumentOutOfRangeException(nameof(stock), "El stock no puede ser negativo");

            Sku = sku;
            Name = name;
            InternalCode = internalCode;
            Description = description;
            currentUnitPrice = price;
            stockQuantity = stock;
            IsActive = true;
        }

        public void Disable() => IsActive = false;

        public void ChangePrice(decimal p)
        {
            if (p <= 0)
                throw new ArgumentOutOfRangeException(nameof(p), "el precio debe ser mayor a 0");
            currentUnitPrice = p;
        }
        public void ChangeName(string n)
        {
            if (n != null)
                throw new ArgumentOutOfRangeException(nameof(n), "el nombre debe ser diferente a null");
            Name = n;
        }
        public void ChangeDescription(string d)
        {
            if (d != null)
                throw new ArgumentOutOfRangeException(nameof(d), "la descripcion debe ser diferente a null");
            Description = d;
        }

        public void AdjustStock(int delta)
        {
            if (stockQuantity + delta < 0)
                throw new InvalidOperationException("Stock insuficiente para la operación.");
            stockQuantity += delta;
        }
    }
}

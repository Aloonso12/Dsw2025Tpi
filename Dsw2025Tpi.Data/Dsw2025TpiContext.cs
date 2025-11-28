using Microsoft.EntityFrameworkCore;
using Dsw2025Tpi.Domain.Entities;
using Microsoft.Identity.Client;
namespace Dsw2025Tpi.Data;

public class Dsw2025TpiContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(b =>
        {
            b.HasKey(p => p.Id);

            b.Property(p => p.Sku)
             .IsRequired()
             .HasMaxLength(50);
            b.HasIndex(p => p.Sku)
             .IsUnique();

            b.Property(p => p.InternalCode)                
            .IsRequired()
            .HasMaxLength(50);

            b.Property(p => p.Name)
             .IsRequired()
             .HasMaxLength(100);

            b.Property(p => p.currentUnitPrice)
             .IsRequired()
             .HasColumnType("decimal(18,2)");

            b.Property(p => p.stockQuantity)
             .IsRequired();

            b.Property(p => p.IsActive)
             .IsRequired();
        });

        modelBuilder.Entity<Customer>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name)
             .IsRequired()
             .HasMaxLength(100);
            b.Property(c => c.Email)
             .IsRequired()
             .HasMaxLength(150);
        });
        modelBuilder.Entity<Order>(b =>
        {
            b.HasKey(o => o.Id);

            b.Property(o => o.CustomerId)
             .IsRequired();

            b.Property(o => o.Date)
             .IsRequired();

            b.Property(o => o.ShippingAddress)
             .IsRequired()
             .HasMaxLength(250);

            b.Property(o => o.BillingAddress)
             .IsRequired()
             .HasMaxLength(250);

            b.Property(o => o.Notes)
             .HasMaxLength(500);

            b.Property(o => o.Status)
             .HasConversion<string>()
             .IsRequired();

            b.HasMany(o => o.Items)
             .WithOne(oi => oi.Order)
             .HasForeignKey(oi => oi.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ToTable("Orders");
        });
        modelBuilder.Entity<OrderItem>(b =>
        {
            b.HasKey(oi => oi.Id);

            b.Property(oi => oi.ProductId)
             .IsRequired();

            b.Property(oi => oi.Quantity)
             .IsRequired();

            b.Property(oi => oi.UnitPrice)
             .IsRequired()
             .HasColumnType("decimal(18,2)");

            b.Ignore(oi => oi.Subtotal);

            b.ToTable("OrderItems");
        });
    }
}
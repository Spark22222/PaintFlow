using Microsoft.EntityFrameworkCore;
using PaintStore.Model.Models;

namespace PaintStore.API.Data;

public class PaintStoreDbContext : DbContext
{
    public PaintStoreDbContext(DbContextOptions<PaintStoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<PaintProduct> PaintProducts { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    // 先不要接 Payment
    // public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Ignore(user => user.PaymentHistory);

        modelBuilder.Ignore<Payment>();

        modelBuilder.Entity<PaintProduct>()
            .OwnsOne(product => product.Brand);

        modelBuilder.Entity<PaintProduct>()
            .OwnsOne(product => product.Specification);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.User)
            .WithMany(user => user.OrderHistory)
            .HasForeignKey(order => order.UserId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.Order)
            .WithMany(order => order.OrderItems)
            .HasForeignKey(item => item.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.PaintProduct)
            .WithMany()
            .HasForeignKey(item => item.PaintProductId);

        modelBuilder.Entity<OrderItem>()
            .Ignore(item => item.Subtotal);

        modelBuilder.Entity<PaintProduct>()
            .Property(product => product.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(order => order.TotalPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderItem>()
            .Property(item => item.UnitPrice)
            .HasColumnType("decimal(18,2)");
    }
}
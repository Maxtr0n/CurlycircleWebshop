using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
      
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .Entity<Order>()
        .Property(o => o.PaymentMethod)
        .HasConversion<string>();

      modelBuilder
        .Entity<Order>()
        .Property(o => o.ShippingMethod)
        .HasConversion<string>();
    }
  }
}

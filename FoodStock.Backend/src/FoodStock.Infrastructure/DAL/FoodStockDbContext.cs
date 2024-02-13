using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Infrastructure.DAL;

internal sealed class FoodStockDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Producent> Producents { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public FoodStockDbContext(DbContextOptions<FoodStockDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}

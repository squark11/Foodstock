using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Infrastructure.DAL.Configurators;

public class OrderConfigurator : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OrderDate)
            .IsRequired();
        builder.Property(x => x.OrderName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.OrderStatus)
            .HasMaxLength(40)
            .IsRequired();

        builder.HasOne(x => x.Supplier)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.SupplierId);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.OrderBy);

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.OrganizationId);

    }
}

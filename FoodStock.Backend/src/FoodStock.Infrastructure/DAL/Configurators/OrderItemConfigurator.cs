using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Infrastructure.DAL.Configurators;

public class OrderItemConfigurator : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.Quantity)
            .IsRequired();
        builder.Property(p => p.BarCode)
            .HasMaxLength(13);

        builder.Property(x => x.Name)
            .HasColumnName("Name");
        builder.Property(x => x.CategoryId)
            .HasColumnName("CategoryId");
        builder.Property(x => x.ProducentId)
            .HasColumnName("ProducentId");
        builder.Property(x => x.BarCode)
            .HasColumnName("BarCode");
        builder.Property(x => x.SupplierId)
            .HasColumnName("SupplierId");
        
        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Category)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Producent)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(p => p.ProducentId);

        builder.HasOne(p => p.Supplier)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(p => p.SupplierId);
        
        
    }
}

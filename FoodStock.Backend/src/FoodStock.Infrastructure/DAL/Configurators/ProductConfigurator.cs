using FoodStock.Core;
using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Infrastructure.DAL.Configurators;

internal sealed class ProductConfigurator : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.ExpirationDate)
            .IsRequired();
        builder.Property(p => p.Quantity)
            .IsRequired();
        builder.Property(p => p.BarCode)
            .HasMaxLength(13);
        builder.Property(p => p.DeliveryDate)
            .IsRequired();
        
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

        builder.HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Producent)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.ProducentId);

        builder.HasOne(p => p.Supplier)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.SupplierId);

        builder.HasOne(p => p.User)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.UserId);
    }
}

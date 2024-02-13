using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Infrastructure.DAL.Configurators;

internal sealed class ProducentConfiguration : IEntityTypeConfiguration<Producent>
{
    public void Configure(EntityTypeBuilder<Producent> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();
    }
}

using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Infrastructure.DAL.Configurators;

public class OrganizationConfigurator : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.OwnerName)
            .IsRequired();
        builder.Property(x => x.OwnerSurname)
            .IsRequired();
        builder.Property(x => x.Nip)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.City)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.CityCode)
            .HasMaxLength(6)
            .IsRequired();
        builder.Property(x => x.Street)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.StreetNumber)
            .HasMaxLength(100)
            .IsRequired();
    }
}

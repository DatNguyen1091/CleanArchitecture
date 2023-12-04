using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(t => t.ProductId);

        builder.Property(t => t.Name).HasMaxLength(255).IsRequired();

        builder.Property(t => t.Description).HasMaxLength(255);

        builder.Property(t => t.Price).IsRequired().HasConversion(typeof(decimal));

        builder.Property(t => t.Quatity).IsRequired().HasAnnotation("Range", new RangeAttribute(0, 1000));

        builder.Property(x => x.CategoryId).IsRequired();
    }
}


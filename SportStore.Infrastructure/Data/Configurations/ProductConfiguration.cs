using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Domain.Entities;

namespace SportStore.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(product => product.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(product => product.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(product => product.ImageUrl)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

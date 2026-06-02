using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Domain.Entities;

namespace SportStore.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new Category { Id = 1, Name = "Soccer" },
            new Category { Id = 2, Name = "Tennis" },
            new Category { Id = 3, Name = "Cycling" },
            new Category { Id = 4, Name = "Swimming" },
            new Category { Id = 5, Name = "Running" },
            new Category { Id = 6, Name = "Basketball" },
            new Category { Id = 7, Name = "Fitness" },
            new Category { Id = 8, Name = "Skiing" },
            new Category { Id = 9, Name = "Yoga" }
        );
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Domain.Entities;

namespace SportStore.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(order => order.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(order => order.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(order => order.Street)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(order => order.Number)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(order => order.Zip)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(order => order.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(order => order.CustomerId)
            .IsRequired();

        builder.HasMany(order => order.OrderItems)
            .WithOne(orderItem => orderItem.Order)
            .HasForeignKey(orderItem => orderItem.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

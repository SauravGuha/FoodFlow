
using FoodFlow.Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class OrderConfiguration : BaseConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(Order));

        builder.HasOne(e => e.Customer)
        .WithMany()
        .HasForeignKey(e => e.CustomerId)
        .IsRequired()
        .OnDelete(DeleteBehavior.NoAction);

        builder.Property(e => e.ShippingCost)
        .IsRequired();

        builder.HasOne(e => e.Branch)
        .WithMany()
        .HasForeignKey(e => e.BranchId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.Property(e => e.Status)
        .HasConversion(o => o.ToString(), o => Enum.Parse<OrderStatus>(o));

        builder.OwnsOne(e => e.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street).IsRequired().HasMaxLength(200);
            addressBuilder.Property(a => a.City).IsRequired().HasMaxLength(100);
            addressBuilder.Property(a => a.State).IsRequired().HasMaxLength(100);
            addressBuilder.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
            addressBuilder.Property(a => a.Country).IsRequired().HasMaxLength(100);
        });
        builder.OwnsOne(e => e.DeliveryAddress, shippingAddressBuilder =>
        {
            shippingAddressBuilder.Property(s => s.Street).IsRequired().HasMaxLength(200);
            shippingAddressBuilder.Property(s => s.City).IsRequired().HasMaxLength(100);
            shippingAddressBuilder.Property(s => s.State).IsRequired().HasMaxLength(100);
            shippingAddressBuilder.Property(s => s.ZipCode).IsRequired().HasMaxLength(20);
            shippingAddressBuilder.Property(s => s.Country).IsRequired().HasMaxLength(100);
        });
        builder.HasMany(e => e.OrderItems)
        .WithOne()
        .HasForeignKey(e => e.OrderId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
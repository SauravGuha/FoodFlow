
using FoodFlow.Domain.Models.InventoryModels;
using FoodFlow.Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class OrderItemConfiguration : BaseConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.HasOne(typeof(BranchInventory))
            .WithMany()
            .HasForeignKey(nameof(OrderItem.BranchInventoryId));

        builder.Property(e => e.Quantity).IsRequired();

        builder.Property(e => e.ItemName).IsRequired();
        builder.Property(e => e.UnitPrice).IsRequired();
        builder.Property(e => e.Sku).IsRequired();
        builder.Property(e => e.TaxCode).IsRequired(false);
        builder.Property(e => e.DiscountPercent).IsRequired();
        builder.Property(e => e.TaxPercent).IsRequired();
    }
}
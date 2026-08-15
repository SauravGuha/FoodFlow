
using FoodFlow.Domain.Models.InventoryModels;
using FoodFlow.Domain.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class BranchInventoryConfiguration : BaseConfiguration<BranchInventory>
{
    public override void Configure(EntityTypeBuilder<BranchInventory> builder)
    {
        base.Configure(builder);

        builder.HasOne<Item>()
        .WithMany()
        .HasForeignKey(e => e.ItemId)
        .IsRequired()
        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

        builder.HasOne<Branch>()
        .WithMany()
        .HasForeignKey(e => e.BranchId)
        .IsRequired()
        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

        builder.Property(e => e.RowVersion)
        .IsRowVersion();
    }
}

using FoodFlow.Domain.Models.InventoryModels;
using FoodFlow.Domain.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class ItemConfiguration : BaseConfiguration<Item>
{
    public override void Configure(EntityTypeBuilder<Item> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
        .IsRequired()
        .HasMaxLength(500);

        builder.Property(e => e.Description)
        .IsRequired()
        .HasMaxLength(1000);

        builder.Property(e => e.Sku)
        .IsRequired()
        .HasMaxLength(30);

        builder.HasOne<Restaurant>()
        .WithMany()
        .HasForeignKey(e => e.RestaurantId)
        .IsRequired()
        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        builder.HasOne<Cuisine>()
        .WithMany()
        .HasForeignKey(e => e.CuisineId)
        .IsRequired()
        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        builder.HasIndex(e =>
        new
        {
            e.RestaurantId,
            e.CuisineId
        })
        .IsUnique();
    }
}
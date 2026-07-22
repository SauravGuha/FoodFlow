
using FoodFlow.Domain.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class CuisineConfiguration : BaseConfiguration<Cuisine>
{
    public override void Configure(EntityTypeBuilder<Cuisine> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
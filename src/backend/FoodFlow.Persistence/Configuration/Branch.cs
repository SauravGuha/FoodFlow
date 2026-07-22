
using System.Text.Json;
using FoodFlow.Domain.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class BranchConfiguration : BaseConfiguration<Branch>
{
    public override void Configure(EntityTypeBuilder<Branch> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.OwnsOne(e => e.AddressDetails, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street).IsRequired().HasMaxLength(200);
            addressBuilder.Property(a => a.City).IsRequired().HasMaxLength(100);
            addressBuilder.Property(a => a.State).IsRequired().HasMaxLength(100);
            addressBuilder.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
        });

        builder.Property(e => e.OperatingHours)
        .HasConversion(e => JsonSerializer.Serialize(e),
        e => JsonSerializer.Deserialize<OperatingHours>(e)!);
    }
}
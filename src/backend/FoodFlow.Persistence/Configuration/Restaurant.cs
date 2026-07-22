
using FoodFlow.Domain.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class RestaurantConfiguration : BaseConfiguration<Restaurant>
{
    public override void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(r => r.RestaurantOwner, owner =>
        {
            owner.Property(o => o.Name).HasColumnName("OwnerName").HasMaxLength(200).IsRequired();
            owner.Property(o => o.Email).HasColumnName("OwnerEmail").HasMaxLength(200).IsRequired();
            owner.Property(o => o.PhoneNumber).HasColumnName("OwnerPhoneNumber").HasMaxLength(20).IsRequired()
            .HasConversion(p => $"{p.CountryCode}-{p.Number}",
            s => new PhoneNumber(s.Split('-')[0], s.Split('-')[1])
            );
        });

        builder.Property(e => e.Status)
        .HasConversion<string>();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(r => r.GstNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.FNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        builder.HasMany(r => r.Cuisines)
        .WithOne()
        .HasForeignKey("RestaurantId")
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Branches)
        .WithOne()
        .HasForeignKey("RestaurantId")
        .OnDelete(DeleteBehavior.Cascade);
    }
}
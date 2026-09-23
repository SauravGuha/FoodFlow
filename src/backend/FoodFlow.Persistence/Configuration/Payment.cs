
using FoodFlow.Domain.Models.PaymentModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class PaymentConfiguration : BaseConfiguration<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        base.Configure(builder);

        builder.HasOne(e => e.Order)
        .WithOne()
        .HasForeignKey<Payment>(e => e.OrderId);

        builder.Property(e => e.ExternalOrderId)
        .HasMaxLength(200)
        .IsRequired();

        builder.Property(e => e.ExternalPaymentId)
        .HasMaxLength(200)
        .IsRequired(false);

        builder.Property(e => e.Status)
        .HasConversion<String>()
        .HasMaxLength(50);

    }
}
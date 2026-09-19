
using FoodFlow.Domain.Models.CustomerModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class CustomerConfiguration : BaseConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.ToTable(nameof(Customer));
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(256);
        builder.Property(e => e.UserName).IsRequired().HasMaxLength(256);
        builder.Property(u => u.ExternalId).IsRequired().HasMaxLength(32);
    }
}
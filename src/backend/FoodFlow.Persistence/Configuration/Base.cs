
using FoodFlow.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Persistence.Configuration;

public class BaseConfiguration<T> : IEntityTypeConfiguration<T>
where T : BaseModel
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.UpdatedAt).IsRequired();

        builder.Property(b => b.CreatedBy)
        .HasMaxLength(300);

        builder.Property(b => b.UpdatedBy)
        .HasMaxLength(300);
    }
}
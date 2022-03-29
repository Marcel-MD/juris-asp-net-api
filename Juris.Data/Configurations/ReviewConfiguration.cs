using Juris.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juris.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasOne(r => r.Profile)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.Description)
            .HasMaxLength(250);

        builder.Property(r => r.Email)
            .HasMaxLength(50);

        builder.Property(r => r.FirstName)
            .HasMaxLength(50);

        builder.Property(r => r.LastName)
            .HasMaxLength(50);

        builder.Property(r => r.FirstName)
            .HasMaxLength(50);

        builder.Property(r => r.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(r => r.CreationDate)
            .HasDefaultValueSql("GETDATE()");
    }
}
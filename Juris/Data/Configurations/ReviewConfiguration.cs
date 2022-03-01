using Juris.Models.Entities;
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
    }
}
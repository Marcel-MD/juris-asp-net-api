using Juris.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juris.Data.Configurations;

public class ExperienceConfigurations : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.HasOne(e => e.Profile)
            .WithMany(p => p.Experiences)
            .HasForeignKey(e => e.ProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
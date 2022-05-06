using Juris.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juris.Dal.Configurations;

public class ExperienceConfigurations : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.HasOne(e => e.Profile)
            .WithMany(p => p.Experiences)
            .HasForeignKey(e => e.ProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Company)
            .HasMaxLength(50);

        builder.Property(e => e.Position)
            .HasMaxLength(50);
        
        builder.Property(e => e.EndDate).IsRequired(false);
    }
}
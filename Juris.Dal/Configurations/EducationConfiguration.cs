using Juris.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juris.Dal.Configurations;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasOne(e => e.Profile)
            .WithMany(p => p.Educations)
            .HasForeignKey(e => e.ProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Institution)
            .HasMaxLength(50);

        builder.Property(e => e.Speciality)
            .HasMaxLength(50);

        builder.Property(e => e.EndDate).IsRequired(false);
    }
}
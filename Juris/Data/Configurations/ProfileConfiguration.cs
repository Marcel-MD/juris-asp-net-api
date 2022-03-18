using Juris.Models.Constants;
using Juris.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juris.Data.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasOne(p => p.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<Profile>(p => p.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.ProfileCategory)
            .WithMany(c => c.Profiles)
            .HasForeignKey(p => p.ProfileCategoryId)
            .IsRequired();

        builder.HasOne(p => p.City)
            .WithMany(c => c.Profiles)
            .HasForeignKey(p => p.CityId)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasDefaultValue(ProfileStatus.Unapproved);
    }
}
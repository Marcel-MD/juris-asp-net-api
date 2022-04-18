using Juris.Domain.Constants;
using Juris.Domain.Entities;
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
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.ProfileCategory)
            .WithMany(c => c.Profiles)
            .HasForeignKey(p => p.ProfileCategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.City)
            .WithMany(c => c.Profiles)
            .HasForeignKey(p => p.CityId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Status)
            .HasMaxLength(50)
            .HasDefaultValue(ProfileStatus.Unapproved);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.FirstName)
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .HasMaxLength(50);

        builder.Property(p => p.FirstName)
            .HasMaxLength(50);

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(p => p.Address)
            .HasMaxLength(150);

        builder.Property(p => p.ImageName)
            .HasMaxLength(250);
    }
}
using Juris.Domain.Constants;
using Juris.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juris.Data.Configurations;

public class AppointmentRequestConfiguration : IEntityTypeConfiguration<AppointmentRequest>
{
    public void Configure(EntityTypeBuilder<AppointmentRequest> builder)
    {
        builder.HasOne(a => a.User)
            .WithMany(u => u.AppointmentRequests)
            .HasForeignKey(a => a.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.Status)
            .HasMaxLength(50)
            .HasDefaultValue(AppointmentStatus.OnHold);

        builder.Property(a => a.Description)
            .HasMaxLength(250);

        builder.Property(a => a.Email)
            .HasMaxLength(50);

        builder.Property(a => a.FirstName)
            .HasMaxLength(50);

        builder.Property(a => a.LastName)
            .HasMaxLength(50);

        builder.Property(a => a.FirstName)
            .HasMaxLength(50);

        builder.Property(a => a.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(a => a.CreationDate)
            .HasDefaultValueSql("GETDATE()");
    }
}
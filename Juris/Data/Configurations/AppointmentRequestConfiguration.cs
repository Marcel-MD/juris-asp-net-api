using Juris.Models.Entities;
using Juris.Models.Enums;
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
            .HasDefaultValue(AppointmentStatus.OnHold);

        builder.Property(a => a.CreationDate)
            .HasDefaultValueSql("GETDATE()");
    }
}
using Juris.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juris.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasOne(a => a.Profile)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.ProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
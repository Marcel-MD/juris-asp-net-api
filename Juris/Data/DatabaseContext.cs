using Juris.Models.Entities;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Juris.Data;

public class DatabaseContext : IdentityDbContext<User, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<AppointmentRequest> AppointmentRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}
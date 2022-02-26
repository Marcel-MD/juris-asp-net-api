using Juris.Models;
using Juris.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Juris.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
}
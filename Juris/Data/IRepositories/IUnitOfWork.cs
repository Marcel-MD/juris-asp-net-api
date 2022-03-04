using Juris.Models.Entities;

namespace Juris.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<AppointmentRequest> AppointmentRequestRepository { get; }
    IGenericRepository<Profile> ProfileRepository { get; }
    IGenericRepository<Review> ReviewRepository { get; }
    IGenericRepository<Education> EducationRepository { get; }
    IGenericRepository<Experience> ExperienceRepository { get; }
    IGenericRepository<Address> AddressRepository { get; }

    Task Save();
}
using Juris.Models.Entities;

namespace Juris.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _dbContext;

    private IGenericRepository<AppointmentRequest> _appointmentRequestRepository;
    private IGenericRepository<City> _cityRepository;
    private bool _disposed;
    private IGenericRepository<Education> _educationRepository;
    private IGenericRepository<Experience> _experienceRepository;
    private IGenericRepository<ProfileCategory> _profileCategoryRepository;
    private IGenericRepository<Profile> _profileRepository;
    private IGenericRepository<Review> _reviewRepository;

    public UnitOfWork(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IGenericRepository<AppointmentRequest> AppointmentRequestRepository =>
        _appointmentRequestRepository ??= new GenericRepository<AppointmentRequest>(_dbContext);

    public IGenericRepository<Profile> ProfileRepository =>
        _profileRepository ??= new GenericRepository<Profile>(_dbContext);

    public IGenericRepository<Review> ReviewRepository =>
        _reviewRepository ??= new GenericRepository<Review>(_dbContext);

    public IGenericRepository<Education> EducationRepository =>
        _educationRepository ??= new GenericRepository<Education>(_dbContext);

    public IGenericRepository<Experience> ExperienceRepository =>
        _experienceRepository ??= new GenericRepository<Experience>(_dbContext);

    public IGenericRepository<ProfileCategory> ProfileCategoryRepository =>
        _profileCategoryRepository ??= new GenericRepository<ProfileCategory>(_dbContext);

    public IGenericRepository<City> CityRepository =>
        _cityRepository ??= new GenericRepository<City>(_dbContext);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _dbContext.Dispose();

        _disposed = true;
    }
}
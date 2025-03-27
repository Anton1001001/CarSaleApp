using File.Core.Abstractions;

namespace File.DataAccess.Repositories;

public class UnitOfWork(IPhotoRepository photoRepository, FileDbContext context) : IUnitOfWork
{
    public IPhotoRepository PhotoRepository { get; } = photoRepository;
    
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
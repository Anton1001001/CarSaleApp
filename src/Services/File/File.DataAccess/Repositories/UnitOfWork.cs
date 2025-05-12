using File.Core.Abstractions;
using Microsoft.Extensions.Logging;

namespace File.DataAccess.Repositories;

public class UnitOfWork(IPhotoRepository photoRepository, FileDbContext context, ILogger<UnitOfWork> logger) : IUnitOfWork
{
    public IPhotoRepository PhotoRepository { get; } = photoRepository;

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Context hash: {hash}", context.GetHashCode());
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
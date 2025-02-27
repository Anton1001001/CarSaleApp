using Advert.Domain.Interfaces;

namespace Advert.Infrastructure.Repositories;

public class UnitOfWork(
    IAdvertRepository advertRepository,
    IAdvertPrivateStatusRepository advertPrivateStatusRepository,
    IAdvertPublicStatusRepository advertPublicStatusRepository,
    AdvertDbContext context)
    : IUnitOfWork
{
    public IAdvertRepository AdvertRepository { get; } = advertRepository;
    public IAdvertPrivateStatusRepository AdvertPrivateStatusRepository { get; } = advertPrivateStatusRepository;
    public IAdvertPublicStatusRepository AdvertPublicStatusRepository { get; } = advertPublicStatusRepository;

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
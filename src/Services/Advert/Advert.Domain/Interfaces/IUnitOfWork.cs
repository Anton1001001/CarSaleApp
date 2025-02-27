namespace Advert.Domain.Interfaces;

public interface IUnitOfWork
{
    IAdvertRepository AdvertRepository { get; }
    IAdvertPrivateStatusRepository AdvertPrivateStatusRepository { get; }
    IAdvertPublicStatusRepository AdvertPublicStatusRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
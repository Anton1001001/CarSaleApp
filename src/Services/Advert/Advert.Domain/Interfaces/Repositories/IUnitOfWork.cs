namespace Advert.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    IAdvertRepository AdvertRepository { get; }
    IAdvertPrivateStatusRepository AdvertPrivateStatusRepository { get; }
    IAdvertPublicStatusRepository AdvertPublicStatusRepository { get; }
    IPlaceRepository PlaceRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
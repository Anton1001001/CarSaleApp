using Advert.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Advert.Infrastructure.Repositories;

public class UnitOfWork(
    IAdvertRepository advertRepository,
    IAdvertPrivateStatusRepository advertPrivateStatusRepository,
    IAdvertPublicStatusRepository advertPublicStatusRepository,
    IAdvertCategoryRepository advertCategoryRepository,
    IPlaceRepository placeRepository,
    IServiceProvider serviceProvider,
    AdvertDbContext context)
    : IUnitOfWork
{
    public IRepository<T> Repository<T>() where T : class
    {
        return serviceProvider.GetRequiredService<IRepository<T>>();
    }
    public IAdvertRepository AdvertRepository { get; } = advertRepository;
    public IAdvertPrivateStatusRepository AdvertPrivateStatusRepository { get; } = advertPrivateStatusRepository;
    public IAdvertPublicStatusRepository AdvertPublicStatusRepository { get; } = advertPublicStatusRepository;
    public IAdvertCategoryRepository AdvertCategoryRepository { get; } = advertCategoryRepository;
    public IPlaceRepository PlaceRepository { get; } = placeRepository;

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
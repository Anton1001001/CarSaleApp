using Advert.Domain.Entities;

namespace Advert.Domain.Interfaces.Repositories;

public interface IAdvertPublicStatusRepository
{
    Task<AdvertPublicStatus?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
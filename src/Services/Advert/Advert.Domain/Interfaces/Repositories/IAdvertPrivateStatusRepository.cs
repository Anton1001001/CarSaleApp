using Advert.Domain.Entities;

namespace Advert.Domain.Interfaces.Repositories;

public interface IAdvertPrivateStatusRepository
{
    Task<AdvertPrivateStatus?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
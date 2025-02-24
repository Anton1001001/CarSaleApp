using Advert.Domain.Entities;

namespace Advert.Domain.Interfaces;

public interface IAdvertPrivateStatusRepository
{
    Task<AdvertPrivateStatus?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
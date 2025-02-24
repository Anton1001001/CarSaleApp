using Advert.Domain.Entities;

namespace Advert.Domain.Interfaces;

public interface IAdvertPublicStatusRepository
{
    Task<AdvertPublicStatus> GetByNameAsync(string name, CancellationToken cancellationToken);
}
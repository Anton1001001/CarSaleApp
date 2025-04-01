using Advert.Domain.Entities;

namespace Advert.Domain.Interfaces.Repositories;

public interface IAdvertCategoryRepository
{
    Task<AdvertCategory?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
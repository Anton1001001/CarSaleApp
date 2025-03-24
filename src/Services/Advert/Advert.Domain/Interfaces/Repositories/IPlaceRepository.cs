using Advert.Domain.Entities;

namespace Advert.Domain.Interfaces.Repositories;

public interface IPlaceRepository : IRepository<Place>
{
    Task<List<Place>> GetPlacesByTypeAsync(string type, CancellationToken cancellationToken);
    Task<List<Place>> GetPlacesByParentIdAsync(int parentId, CancellationToken cancellationToken);
}
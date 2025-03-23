using Advert.Domain.Entities;
using Advert.Domain.Interfaces;
using Advert.Domain.Interfaces.Repositories;

namespace Advert.Infrastructure.Repositories;

public class PlaceRepository(IRepository<Place> repository, AdvertDbContext context) : IPlaceRepository 
{

    public async Task<List<Place>> GetPlacesByTypeAsync(string type)
    {
        var response = context.Places
            .Where(place => place.Type == type)
            .OrderBy(place => place.Id)
            .ToList();
        return await Task.FromResult(response);
    }

    public async Task<List<Place>> GetPlacesByParentIdAsync(int parentId)
    {
        var response = context.Places
            .Where(place => place.ParentId == parentId)
            .OrderBy(place => place.Id)
            .ToList();
        return await Task.FromResult(response);
    }

    public Task<Place?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return repository.GetByIdAsync(id, cancellationToken);
    }

    public Task<List<Place>> GetAllAsync(CancellationToken cancellationToken)
    {
        return repository.GetAllAsync(cancellationToken);
    }
}
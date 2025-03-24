using Advert.Domain.Entities;
using Advert.Domain.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Advert.Infrastructure.Repositories;

public class PlaceRepository(IRepository<Place> repository, AdvertDbContext context) : IPlaceRepository 
{

    public async Task<List<Place>> GetPlacesByTypeAsync(string type, CancellationToken cancellationToken = default)
    {
        var response = await context.Places
            .Where(place => place.Type == type)
            .OrderBy(place => place.Id)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return response;
    }

    public async Task<List<Place>> GetPlacesByParentIdAsync(int parentId, CancellationToken cancellationToken = default)
    {
        var response = await context.Places
            .Where(place => place.ParentId == parentId)
            .OrderBy(place => place.Id)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return response;
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
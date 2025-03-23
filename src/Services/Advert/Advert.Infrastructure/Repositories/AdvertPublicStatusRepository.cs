using Advert.Domain.Entities;
using Advert.Domain.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Advert.Infrastructure.Repositories;

public class AdvertPublicStatusRepository(AdvertDbContext context) : IAdvertPublicStatusRepository
{
    public async Task<AdvertPublicStatus?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.AdvertPublicStatuses.FirstOrDefaultAsync(publicStatus => publicStatus.Name == name, cancellationToken); 
    }
}
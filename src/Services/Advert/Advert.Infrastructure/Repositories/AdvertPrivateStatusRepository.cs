using Advert.Domain.Entities;
using Advert.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advert.Infrastructure.Repositories;

public class AdvertPrivateStatusRepository(AdvertDbContext context) : IAdvertPrivateStatusRepository
{
    public async Task<AdvertPrivateStatus?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.AdvertPrivateStatuses.FirstOrDefaultAsync(privateStatus => privateStatus.Name == name, cancellationToken);
    }
}
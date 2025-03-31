using Advert.Domain.Entities;
using Advert.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Advert.Infrastructure.Repositories;

public class AdvertCategoryRepository(IRepository<AdvertCategory> baseRepository, AdvertDbContext context) : IAdvertCategoryRepository
{
    public async Task<AdvertCategory?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await context.AdvertCategories.FirstOrDefaultAsync(category => category.Name == name, cancellationToken);
    }
}
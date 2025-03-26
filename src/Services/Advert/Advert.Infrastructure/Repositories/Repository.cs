using Advert.Domain.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Advert.Infrastructure.Repositories;


public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet;
    protected readonly AdvertDbContext Context;

    public Repository(AdvertDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }
}
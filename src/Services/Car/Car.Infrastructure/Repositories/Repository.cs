namespace Car.Infrastructure.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet;
    protected readonly CarInfoDbContext Context;

    public Repository(CarInfoDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}
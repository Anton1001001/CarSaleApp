namespace Car.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync();
}

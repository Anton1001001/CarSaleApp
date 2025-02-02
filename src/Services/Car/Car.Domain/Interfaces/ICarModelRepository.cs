namespace Car.Domain.Interfaces;

public interface ICarModelRepository : IRepository<CarModel>
{
    Task<List<CarGeneration>> GetGenerationsAsync(int modelId, CancellationToken cancellationToken);
}
namespace Car.Domain.Interfaces;

public interface ICarModelRepository : IRepository<CarModel>
{
    // Task<List<CarGeneration>> GetGenerationsByYear(int modelId, int year, CancellationToken cancellationToken);
    Task<List<CarGeneration>> GetGenerationsAsync(int modelId, CancellationToken cancellationToken, int? year = null);
}
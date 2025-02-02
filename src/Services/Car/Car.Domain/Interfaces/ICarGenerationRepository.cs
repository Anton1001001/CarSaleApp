namespace Car.Domain.Interfaces;

public interface ICarGenerationRepository : IRepository<CarGeneration>
{
    Task<List<CarModification>> GetModificationsAsync(int generationId, CancellationToken cancellationToken);
}
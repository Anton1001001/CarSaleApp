namespace Car.Infrastructure.Repositories;

public class CarGenerationRepository(CarInfoDbContext context)
    : Repository<CarGeneration>(context), ICarGenerationRepository
{
    public async Task<List<CarModification>> GetModificationsAsync(int generationId, CancellationToken cancellationToken)
    {
        return await Context.CarModifications
            .AsNoTracking()
            .Where(modification => modification.CarSerieNavigation.CarGenerationId == generationId)
            .Include(modification => modification.CarSerieNavigation)
                .ThenInclude(carSerie => carSerie.CarBodyTypeNavigation)
            .Include(modification => modification.CarDriveTypeNavigation)
            .Include(modification => modification.CarEngineTypeNavigation)
            .Include(modification => modification.CarTransmissionTypeNavigation)
            .ToListAsync(cancellationToken);
    }
}
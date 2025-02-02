namespace Car.Infrastructure.Repositories;

public class CarModelRepository(CarInfoDbContext context) : Repository<CarModel>(context), ICarModelRepository
{
    public async Task<List<CarGeneration>> GetGenerationsAsync(int modelId, CancellationToken cancellationToken = default)
    {
        var result = await Context.CarGenerations
            .AsNoTracking()
            .Where(carGeneration => carGeneration.CarModelId == modelId)
            .Include(carGeneration => carGeneration.CarSeries)
                .ThenInclude(carSerie => carSerie.CarBodyTypeNavigation)
            .ToListAsync(cancellationToken);
        
        return result;
    }
}
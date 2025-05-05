namespace Car.Infrastructure.Repositories;

public class CarModelRepository(CarInfoDbContext context) : Repository<CarModel>(context), ICarModelRepository
{
    public async Task<List<CarGeneration>> GetGenerationsAsync(int modelId, CancellationToken cancellationToken = default, int? year = null)
    {
        var query = Context.CarGenerations
            .AsNoTracking()
            .Where(carGeneration => carGeneration.CarModelId == modelId);

        if (year.HasValue)
        {
            query = query.Where(carGeneration => carGeneration.YearBegin <= year
                                                 && (carGeneration.YearEnd ?? DateTime.Now.Year) >= year);
        }

        return await query
            .Include(carGeneration => carGeneration.CarModifications)
            .ThenInclude(carModification => carModification.CarBodyType)
            .ToListAsync(cancellationToken);
    }
}
namespace Car.Infrastructure.Repositories;

public class CarBrandRepository(CarInfoDbContext context) : Repository<CarBrand>(context), ICarBrandRepository
{
    public async Task<IEnumerable<CarModel>> GetModelsAsync(int brandId, CancellationToken cancellationToken)
    {
        return await Context.CarModels
            .AsNoTracking()
            .Where(carModel => carModel.CarBrandId == brandId)
            .ToListAsync(cancellationToken);
    }
}
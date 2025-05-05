namespace Car.Infrastructure.Repositories;

public class CarBrandRepository(IRepository<CarBrand> repository, CarInfoDbContext context) : ICarBrandRepository
{
    public async Task<IEnumerable<CarModel>> GetModelsAsync(int brandId, CancellationToken cancellationToken)
    {
        return await context.CarModels
            .AsNoTracking()
            .Where(carModel => carModel.CarBrandId == brandId)
            .ToListAsync(cancellationToken);
    }

    public Task<CarBrand?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return repository.GetByIdAsync(id, cancellationToken);
    }

    public Task<List<CarBrand>> GetAllAsync()
    {
        return repository.GetAllAsync();
    }
}
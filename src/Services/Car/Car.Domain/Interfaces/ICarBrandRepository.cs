namespace Car.Domain.Interfaces;

public interface ICarBrandRepository : IRepository<CarBrand>
{
    Task<IEnumerable<CarModel>> GetModelsAsync(int brandId, CancellationToken cancellationToken);
}
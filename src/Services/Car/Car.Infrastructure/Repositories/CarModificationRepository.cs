namespace Car.Infrastructure.Repositories;

public class CarModificationRepository(CarInfoDbContext context)
    : Repository<CarModification>(context), ICarModificationRepository
{
    public async Task<List<CarCharacteristicValue>> GetCharacteristicsAsync(int modificationId, CancellationToken cancellationToken)
    {
        var result = await Context.CarCharacteristicValues
            .AsNoTracking()
            .Where(characteristicValue => characteristicValue.CarModificationId == modificationId)
            .Include(characteristicValue => characteristicValue.CarCharacteristicNavigation)
                .ThenInclude(c => c.ParentNavigation)
            .ToListAsync(cancellationToken);
        
        return result;
    }
}
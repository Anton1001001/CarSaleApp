namespace Car.Domain.Interfaces;

public interface ICarModificationRepository : IRepository<CarModification>
{
    Task<List<CarCharacteristicValue>> GetCharacteristicsAsync(int modificationId, CancellationToken cancellationToken);
}
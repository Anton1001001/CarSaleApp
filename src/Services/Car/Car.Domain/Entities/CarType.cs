namespace Car.Domain.Entities;

public class CarType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<CarCharacteristicValue> CarCharacteristicValues { get; set; } =
        new List<CarCharacteristicValue>();

    public ICollection<CarCharacteristic> CarCharacteristics { get; set; } = new List<CarCharacteristic>();
    public ICollection<CarEquipment> CarEquipments { get; set; } = new List<CarEquipment>();
    public ICollection<CarGeneration> CarGenerations { get; set; } = new List<CarGeneration>();
    public ICollection<CarBrand> CarBrands { get; set; } = new List<CarBrand>();
    public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    public ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
    public ICollection<CarOptionValue> CarOptionValues { get; set; } = new List<CarOptionValue>();
    public ICollection<CarOption> CarOptions { get; set; } = new List<CarOption>();
    public ICollection<CarSerie> CarSeries { get; set; } = new List<CarSerie>();
}
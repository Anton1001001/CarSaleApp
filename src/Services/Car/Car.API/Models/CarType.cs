namespace Car.API.Models;
public class CarType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<CarCharacteristicValue> CarCharacteristicValues { get; set; } = new List<CarCharacteristicValue>();
    public virtual ICollection<CarCharacteristic> CarCharacteristics { get; set; } = new List<CarCharacteristic>();
    public virtual ICollection<CarEquipment> CarEquipments { get; set; } = new List<CarEquipment>();
    public virtual ICollection<CarGeneration> CarGenerations { get; set; } = new List<CarGeneration>();
    public virtual ICollection<CarBrand> CarBrands { get; set; } = new List<CarBrand>();
    public virtual ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    public virtual ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
    public virtual ICollection<CarOptionValue> CarOptionValues { get; set; } = new List<CarOptionValue>();
    public virtual ICollection<CarOption> CarOptions { get; set; } = new List<CarOption>();
    public virtual ICollection<CarSerie> CarSeries { get; set; } = new List<CarSerie>();
}

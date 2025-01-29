namespace Car.API.Models;
public class CarSerie
{
    public int Id { get; set; }
    public int CarModelId { get; set; }
    public int? CarBodyTypeId { get; set; }
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int? CarGenerationId { get; set; }
    public int CarTypeId { get; set; }
}

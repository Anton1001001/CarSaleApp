namespace Advert.Application.Common.Cars;

public class CarsCatalogRequest
{
    public int BrandId { get; set; }
    public int ModelId { get; set; }
    public int BodyTypeId { get; set; }
    public int GenerationId { get; set; }
    public int ModificationId { get; set; }
    public int EngineTypeId { get; set; }
    public int DriveTypeId { get; set; }
    public int TransmissionTypeId { get; set; }
    public int ColorId { get; set; }
    public int ConditionId { get; set; }
    public int InteriorMaterialId { get; set; }
    public int InteriorColorId { get; set; }
}
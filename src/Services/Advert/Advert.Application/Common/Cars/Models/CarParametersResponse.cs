namespace Advert.Application.Common.Cars.Models;

public class CarParametersResponse : IVehicleParametersResponse
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Generation { get; set; }
    public int Year { get; set; }
    public string EngineType { get; set; }
    public string TransmissionType { get; set; }
    public string GenerationWithYears { get; set; }
    public string InteriorMaterial { get; set; }
    public string InteriorColor { get; set; }
    public string BodyType { get; set; }
    public string DriveType { get; set; }
    public string Modification { get; set; }
    public string Color { get; set; }
    public int MileageKm { get; set; }
    public string Condition { get; set; }
    public string? Registration { get; set; }
}
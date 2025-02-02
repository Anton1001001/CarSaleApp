namespace Car.Application.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CarEngineType, EngineTypeResponse>();
        CreateMap<CarDriveType, DriveTypeResponse>();
        CreateMap<CarTransmissionType, TransmissionTypeResponse>();
        CreateMap<CarBodyType, BodyTypeResponse>();
    }
}
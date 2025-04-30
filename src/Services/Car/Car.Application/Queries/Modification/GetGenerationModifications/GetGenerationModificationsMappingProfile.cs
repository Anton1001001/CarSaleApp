namespace Car.Application.Queries.Modification.GetGenerationModifications;

public class GetGenerationModificationsMappingProfile : Profile
{
    public GetGenerationModificationsMappingProfile()
    {
        CreateMap<CarModification, GetGenerationModificationsResponse>()
            .ForCtorParam(dest => dest.EngineType, opt =>
                opt.MapFrom(src => src.CarEngineType))
            .ForCtorParam(dest => dest.DriveType, opt =>
                opt.MapFrom(src => src.CarDriveType))
            .ForCtorParam(dest => dest.TransmissionType, opt =>
                opt.MapFrom(src => src.CarTransmissionType))
            .ForCtorParam(dest => dest.BodyType, opt =>
                opt.MapFrom(src => src.CarBodyType));
    }
}
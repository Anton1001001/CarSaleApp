namespace Car.Application.Queries.GetGenerationModifications;

public class GetGenerationModificationsMappingProfile : Profile
{
    public GetGenerationModificationsMappingProfile()
    {
        CreateMap<CarModification, GetGenerationModificationsResponse>()
            .ForCtorParam(dest => dest.EngineType, opt =>
                opt.MapFrom(src => src.CarEngineTypeNavigation))
            .ForCtorParam(dest => dest.DriveType, opt =>
                opt.MapFrom(src => src.CarDriveTypeNavigation))
            .ForCtorParam(dest => dest.TransmissionType, opt =>
                opt.MapFrom(src => src.CarTransmissionTypeNavigation))
            .ForCtorParam(dest => dest.BodyType, opt =>
                opt.MapFrom(src => src.CarSerieNavigation.CarBodyTypeNavigation));
    }
}
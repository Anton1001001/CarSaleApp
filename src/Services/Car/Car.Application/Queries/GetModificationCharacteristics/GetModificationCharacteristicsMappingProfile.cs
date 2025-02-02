namespace Car.Application.Queries.GetModificationCharacteristics;

public class GetModificationCharacteristicsMappingProfile : Profile
{
    public GetModificationCharacteristicsMappingProfile()
    {
        CreateMap<CarCharacteristic, CharacteristicResponse>();
        CreateMap<CarCharacteristicValue, CharacteristicValueResponse>()
            .ForCtorParam(dest => dest.Characteristic, opt =>
                opt.MapFrom(src => src.CarCharacteristicNavigation));
    }
}
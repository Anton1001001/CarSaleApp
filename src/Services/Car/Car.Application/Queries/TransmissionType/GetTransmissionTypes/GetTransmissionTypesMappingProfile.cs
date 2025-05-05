namespace Car.Application.Queries.TransmissionType.GetTransmissionTypes;

public class GetTransmissionTypesMappingProfile : Profile
{
    public GetTransmissionTypesMappingProfile()
    {
        CreateMap<CarTransmissionType, GetTransmissionTypesResponse>();
    }
}
namespace Car.Application.Queries.GetTransmissionTypes;

public class GetTransmissionTypesMappingProfile : Profile
{
    public GetTransmissionTypesMappingProfile()
    {
        CreateMap<CarTransmissionType, GetTransmissionTypesResponse>();
    }
}
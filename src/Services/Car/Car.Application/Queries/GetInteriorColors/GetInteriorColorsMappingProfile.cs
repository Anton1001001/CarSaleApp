namespace Car.Application.Queries.GetInteriorColors;

public class GetInteriorColorsMappingProfile : Profile
{
    public GetInteriorColorsMappingProfile()
    {
        CreateMap<CarInteriorColor, GetInteriorColorsResponse>();
    }
}
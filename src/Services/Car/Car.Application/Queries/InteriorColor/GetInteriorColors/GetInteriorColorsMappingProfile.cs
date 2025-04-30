namespace Car.Application.Queries.InteriorColor.GetInteriorColors;

public class GetInteriorColorsMappingProfile : Profile
{
    public GetInteriorColorsMappingProfile()
    {
        CreateMap<CarInteriorColor, GetInteriorColorsResponse>();
    }
}
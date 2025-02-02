namespace Car.Application.Queries.GetColors;

public class GetColorsMappingProfile : Profile
{
    public GetColorsMappingProfile()
    {
        CreateMap<CarColor, GetColorsResponse>();
    }
}
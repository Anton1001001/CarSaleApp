namespace Car.Application.Queries.Color.GetColors;

public class GetColorsMappingProfile : Profile
{
    public GetColorsMappingProfile()
    {
        CreateMap<CarColor, GetColorsResponse>();
    }
}
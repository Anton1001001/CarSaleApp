namespace Car.Application.Queries.GetModelGenerations;

public class GetModelGenerationsMappingProfile : Profile
{
    public GetModelGenerationsMappingProfile()
    {
        CreateMap<CarGeneration, GetModelGenerationsResponse>()
            .ForCtorParam(dest => dest.CarBodyTypes, opt =>
                opt.MapFrom(src => src.CarSeries.Select(carSerie => carSerie.CarBodyTypeNavigation)));
    }
}
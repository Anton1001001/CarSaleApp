namespace Car.Application.Queries.Generation.GetModelGenerations;

public class GetModelGenerationsMappingProfile : Profile
{
    public GetModelGenerationsMappingProfile()
    {
        CreateMap<CarGeneration, GetModelGenerationsResponse>()
            .ForCtorParam(dest => dest.CarBodyTypes, opt =>
                opt.MapFrom(src => src.CarModifications
                    .Select(carModification => carModification.CarBodyType)
                    .DistinctBy(bodyType => bodyType.Id)));
    }
}
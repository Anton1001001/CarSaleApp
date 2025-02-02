namespace Car.Application.Queries.GetConditions;

public class GetConditionsMappingProfile : Profile
{
    public GetConditionsMappingProfile()
    {
        CreateMap<CarCondition, GetConditionsResponse>();
    }
}
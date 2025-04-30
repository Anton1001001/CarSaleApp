namespace Car.Application.Queries.Condition.GetConditions;

public class GetConditionsMappingProfile : Profile
{
    public GetConditionsMappingProfile()
    {
        CreateMap<CarCondition, GetConditionsResponse>();
    }
}
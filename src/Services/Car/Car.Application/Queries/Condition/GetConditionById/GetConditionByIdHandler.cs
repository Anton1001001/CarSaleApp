namespace Car.Application.Queries.Condition.GetConditionById;

public class GetConditionByIdHandler(IRepository<CarCondition> conditionRepository) : IRequestHandler<GetConditionByIdQuery, GetConditionByIdResponse>
{
    public async Task<GetConditionByIdResponse> Handle(GetConditionByIdQuery request, CancellationToken cancellationToken)
    {
        var condition = await conditionRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetConditionByIdResponse(condition.Id, condition.Name);
    }
}
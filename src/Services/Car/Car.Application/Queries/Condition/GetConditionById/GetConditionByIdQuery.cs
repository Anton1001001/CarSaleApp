namespace Car.Application.Queries.Condition.GetConditionById;

public record GetConditionByIdQuery(int Id) : IRequest<GetConditionByIdResponse>;
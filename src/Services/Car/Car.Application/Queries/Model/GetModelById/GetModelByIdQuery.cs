namespace Car.Application.Queries.Model.GetModelById;

public record GetModelByIdQuery(int Id) : IRequest<GetModelByIdResponse>;
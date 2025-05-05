namespace Car.Application.Queries.Color.GetColorById;

public record GetColorByIdQuery(int Id) : IRequest<GetColorByIdResponse>;
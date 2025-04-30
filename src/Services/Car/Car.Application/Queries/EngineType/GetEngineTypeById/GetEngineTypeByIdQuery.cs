namespace Car.Application.Queries.EngineType.GetEngineTypeById;

public record GetEngineTypeByIdQuery(int Id) : IRequest<GetEngineTypeByIdResponse>;
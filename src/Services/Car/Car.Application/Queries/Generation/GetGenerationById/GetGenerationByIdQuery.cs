namespace Car.Application.Queries.Generation.GetGenerationById;

public record GetGenerationByIdQuery(int Id) : IRequest<GetGenerationByIdResponse>;
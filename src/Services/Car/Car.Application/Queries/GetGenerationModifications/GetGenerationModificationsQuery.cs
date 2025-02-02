namespace Car.Application.Queries.GetGenerationModifications;

public record GetGenerationModificationsQuery(int GenerationId) : IRequest<List<GetGenerationModificationsResponse>>;
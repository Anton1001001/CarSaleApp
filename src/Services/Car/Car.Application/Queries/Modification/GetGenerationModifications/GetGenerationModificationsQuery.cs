namespace Car.Application.Queries.Modification.GetGenerationModifications;

public record GetGenerationModificationsQuery(int GenerationId) : IRequest<List<GetGenerationModificationsResponse>>;
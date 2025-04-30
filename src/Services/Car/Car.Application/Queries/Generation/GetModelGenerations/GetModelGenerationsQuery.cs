namespace Car.Application.Queries.Generation.GetModelGenerations;

public record GetModelGenerationsQuery(int ModelId, int? Year = null) : IRequest<List<GetModelGenerationsResponse>>;

namespace Car.Application.Queries.GetModelGenerations;

public record GetModelGenerationsQuery(int ModelId) : IRequest<List<GetModelGenerationsResponse>>;

namespace Car.Application.Queries.GetBrandModels;

public record GetBrandModelsQuery(int BrandId) : IRequest<List<GetBrandModelsResponse>>;
namespace Car.Application.Queries.Model.GetBrandModels;

public record GetBrandModelsQuery(int BrandId) : IRequest<List<GetBrandModelsResponse>>;
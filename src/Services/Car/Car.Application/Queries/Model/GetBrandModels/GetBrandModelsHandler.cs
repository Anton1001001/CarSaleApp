namespace Car.Application.Queries.Model.GetBrandModels;

public class GetBrandModelsHandler(ICarBrandRepository brandRepository, IMapper mapper) : IRequestHandler<GetBrandModelsQuery, List<GetBrandModelsResponse>>
{
    public async Task<List<GetBrandModelsResponse>> Handle(GetBrandModelsQuery request, CancellationToken cancellationToken)
    {
        var models = await brandRepository.GetModelsAsync(request.BrandId, cancellationToken);
        var result = mapper.Map<List<GetBrandModelsResponse>>(models);
        return result;
    }
}
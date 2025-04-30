using Advert.Application.Common.Cars.Models;
using Advert.Application.CQRS.Queries.GetAdvertForm.Models;

namespace Advert.Application.Abstractions.GrpcClients;

public interface ICarCatalogGrpcClient
{
    Task<List<GenerationResponse>> GetModelGenerationsAsync(int modelId, CancellationToken cancellationToken, int? year = null);
    Task<List<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken);
    Task<List<ModelResponse>> GetBrandModelsAsync(int brandId, CancellationToken cancellationToken);
    Task<CarsCatalogResponse> GetCarParametersAsync(CarsCatalogRequest request, CancellationToken cancellationToken);
    Task<CarsCatalogPreviewResponse> GetCarParametersPreviewAsync(CarsCatalogPreviewRequest request, CancellationToken cancellationToken);
    Task<List<ModificationResponse>> GetGenerationModificationsAsync(int generationId, CancellationToken cancellationToken);
    Task<List<ColorResponse>> GetColorsAsync(CancellationToken cancellationToken);
    Task<List<InteriorColorResponse>> GetInteriorColorsAsync(CancellationToken cancellationToken);
    Task<List<InteriorMaterialResponse>> GetInteriorMaterialsAsync(CancellationToken cancellationToken);
    Task<List<YearResponse>> GetModelYearsAsync(int modelId, CancellationToken cancellationToken);
}
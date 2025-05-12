using Advert.Application.Abstractions.GrpcClients;
using Advert.Application.Common.Cars.Models;
using Advert.Application.CQRS.Queries.GetAdvertForm.Models;
using AutoMapper;
using Car.GrpcService;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Car.GrpcService.CarCatalog;

namespace Advert.Infrastructure.GrpcClients.CarsCatalog;

public class CarCatalogGrpcClient(ILogger<CarCatalogGrpcClient> logger,CarCatalogClient carCatalogClient, IMapper mapper) : ICarCatalogGrpcClient
{
    public async Task<List<GenerationResponse>> GetModelGenerationsAsync(int modelId,
        CancellationToken cancellationToken = default, int? year = null)
    {
        var request = new GetModelGenerationsRequest { ModelId = modelId };

        if (year.HasValue)
        {
            request.Year = year.Value;
        }

        var catalogResponse =
            await carCatalogClient.GetModelGenerationsAsync(request, cancellationToken: cancellationToken);
        var response = mapper.Map<List<GenerationResponse>>(catalogResponse.Generations);

        return response;
    }

    public async Task<List<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken = default)
    {
        var catalogResponse = await carCatalogClient.GetBrandsAsync(new Empty(), cancellationToken: cancellationToken);
        var response = mapper.Map<List<BrandResponse>>(catalogResponse.Brands);

        return response;
    }

    public async Task<List<ModelResponse>> GetBrandModelsAsync(int brandId,
        CancellationToken cancellationToken = default)
    {
        var catalogResponse = await carCatalogClient
            .GetBrandModelsAsync(new GetBrandModelsRequest { BrandId = brandId },
                cancellationToken: cancellationToken);
        var response = mapper.Map<List<ModelResponse>>(catalogResponse.Models);

        return response;
    }

    public async Task<CarsCatalogResponse> GetCarParametersAsync(CarsCatalogRequest request,
        CancellationToken cancellationToken = default)
    {
        var catalogRequest = mapper.Map<GetCarParametersRequest>(request);
        var json = JsonConvert.SerializeObject(catalogRequest);
        logger.LogInformation("CatalogRequest: {CatalogRequest}", json);
        var response = await carCatalogClient
            .GetCarParametersAsync(catalogRequest, cancellationToken: cancellationToken);

        var responseJson = JsonConvert.SerializeObject(response);
        logger.LogInformation("CatalogResponse: {CatalogResponse}", responseJson);
        
        var mappedResponse = mapper.Map<CarsCatalogResponse>(response);
        var mappedResponseJson = JsonConvert.SerializeObject(mappedResponse);
        logger.LogInformation("MappedCatalogResponse: {MappedResponseJson}", mappedResponseJson);
        return mappedResponse;
    }

    public async Task<CarsCatalogPreviewResponse> GetCarParametersPreviewAsync(CarsCatalogPreviewRequest request,
        CancellationToken cancellationToken)
    {
        var catalogPreviewRequest = mapper.Map<GetCarParametersPreviewRequest>(request);
        var response = await carCatalogClient
            .GetCarParametersPreviewAsync(catalogPreviewRequest, cancellationToken: cancellationToken);
        
        return mapper.Map<CarsCatalogPreviewResponse>(response);
    }

    public async Task<List<ModificationResponse>> GetGenerationModificationsAsync(int generationId,
        CancellationToken cancellationToken = default)
    {
        var catalogResponse = await carCatalogClient.GetGenerationModificationsAsync(
            new GetGenerationModificationsRequest
                { GenerationId = generationId }, cancellationToken: cancellationToken);

        var response = mapper.Map<List<ModificationResponse>>(catalogResponse.Modifications);

        return response;
    }

    public async Task<List<ColorResponse>> GetColorsAsync(CancellationToken cancellationToken = default)
    {
        var catalogResponse = await carCatalogClient.GetColorsAsync(new Empty(), cancellationToken: cancellationToken);
        var response = mapper.Map<List<ColorResponse>>(catalogResponse.Colors);

        return response;
    }

    public async Task<List<InteriorColorResponse>> GetInteriorColorsAsync(CancellationToken cancellationToken = default)
    {
        var catalogResponse =
            await carCatalogClient.GetInteriorColorsAsync(new Empty(), cancellationToken: cancellationToken);
        var response = mapper.Map<List<InteriorColorResponse>>(catalogResponse.InteriorColors);

        return response;
    }

    public async Task<List<InteriorMaterialResponse>> GetInteriorMaterialsAsync(
        CancellationToken cancellationToken = default)
    {
        var catalogResponse =
            await carCatalogClient.GetInteriorMaterialsAsync(new Empty(), cancellationToken: cancellationToken);
        var response = mapper.Map<List<InteriorMaterialResponse>>(catalogResponse.InteriorMaterials);

        return response;
    }

    public async Task<List<YearResponse>> GetModelYearsAsync(int modelId, CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Getting years for model with ID: {ModelId}", modelId);

            var catalogResponse = await carCatalogClient.GetModelYearsAsync(new GetModelYearsRequest { ModelId = modelId },
                cancellationToken: cancellationToken);
            var response = mapper.Map<List<YearResponse>>(catalogResponse.Years);

            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occurred while building car advert form.");
            return new List<YearResponse>();
        }

    }
}
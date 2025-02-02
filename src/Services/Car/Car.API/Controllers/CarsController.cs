namespace Car.API.Controllers;

[ApiController]
[Route("api/cars/catalog")]
public class CarsController(ISender sender, CarInfoDbContext context) : ControllerBase
{
    [HttpGet("brand-items")]
    public async Task<Ok<List<GetBrandsResponse>>> GetBrands(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetBrandsQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }


    [HttpGet("body-types")]
    public async Task<Ok<List<GetBodyTypesResponse>>> GetBodyTypes(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetBodyTypesQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpGet("brand-items/{brandId}/models")]
    public async Task<Ok<List<GetBrandModelsResponse>>> GetBrandModels(int brandId,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetBrandModelsQuery(brandId), cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpGet("colors")]
    public async Task<Ok<List<GetColorsResponse>>> GetColors(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetColorsQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpGet("conditions")]
    public async Task<Ok<List<GetConditionsResponse>>> GetConditions(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetConditionsQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpGet("drive-types")]
    public async Task<Ok<List<GetDriveTypesResponse>>> GetDriveTypes(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetDriveTypesQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpGet("engine-types")]
    public async Task<Ok<List<GetEngineTypesResponse>>> GetEngineTypes(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetEngineTypesQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpGet("models/{modelId}/generations")]
    public async Task<Ok<List<GetModelGenerationsResponse>>> GetGenerations(int modelId,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetModelGenerationsQuery(modelId), cancellationToken);
        return TypedResults.Ok(result);
    }
    
    [HttpGet("interior/colors")]
    public async Task<Ok<List<GetInteriorColorsResponse>>> GetCarInteriorColors(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetInteriorColorsQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }
    
    [HttpGet("interior/materials")]
    public async Task<Ok<List<GetInteriorMaterialsResponse>>> GetInteriorMaterials(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetInteriorMaterialsQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }
    
    [HttpGet("generations/{generationId}/modifications")]
    public async Task<Ok<List<GetGenerationModificationsResponse>>> GetGenerationModifications(int generationId,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetGenerationModificationsQuery(generationId), cancellationToken);
        return TypedResults.Ok(result);
    }
    
    [HttpGet("transmission-types")]
    public async Task<Ok<List<GetTransmissionTypesResponse>>> GetTransmissionTypes(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetTransmissionTypesQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpGet("exchange/types")]
    public async Task<Ok<List<GetExchangeTypesResponse>>> GetExchangeTypes(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetExchangeTypesQuery(), cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpGet("modifications/{modificationId}/characteristics")]
    public async Task<Ok<List<GetModificationCharacteristicsResponse>>> GetModificationCharacteristics(
        int modificationId, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetModificationCharacteristicsQuery(modificationId), cancellationToken);
        return TypedResults.Ok(result);
    }
}


















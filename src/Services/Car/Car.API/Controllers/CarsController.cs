namespace Car.API.Controllers;

[ApiController]
[Route("api/cars/catalog")]
public class CarsController(CarInfoDbContext context) : ControllerBase
{
    [HttpGet("brand-items")]
    public async Task<Ok<List<GetBrandsResponse>>> GetBrands()
    {
        var items = await context.CarBrands
            .Select(carBrand => new GetBrandsResponse(carBrand.Id, carBrand.Name))
            .ToListAsync();
        return TypedResults.Ok(items);
    }

    [HttpGet("body-types")]
    public async Task<Ok<List<GetBodyTypesResponse>>> GetBodyTypes()
    {
        var items = await context.CarBodyTypes
            .Select(bodyType => new GetBodyTypesResponse(bodyType.Id, bodyType.Name)).ToListAsync();
        return TypedResults.Ok(items);
    }
    
    [HttpGet("brand-items/{brandId}/models")]
    public async Task<Ok<List<GetCarModelsByBrandResponse>>> GetCarModelsByBrand(int brandId)
    {
        var items = await context.CarModels
            .Where(carModel => carModel.CarBrandId == brandId)
            .Select(carModel => new GetCarModelsByBrandResponse(carModel.Id, carModel.Name))
            .ToListAsync();
        return TypedResults.Ok(items);
    }

    [HttpGet("colors")]
    public async Task<Ok<List<GetCarColorsResponse>>> GetCarColors()
    {
        var items = await context.CarColors
            .Select(carColor => new GetCarColorsResponse(carColor.Id, carColor.Name))
            .ToListAsync();
        return TypedResults.Ok(items);
    }

    [HttpGet("conditions")]
    public async Task<Ok<List<GetCarConditionsResponse>>> GetCarConditions()
    {
        var items = await context.CarConditions
            .Select(carCondition => new GetCarConditionsResponse(carCondition.Id, carCondition.Name))
            .ToListAsync();
        return TypedResults.Ok(items);
    }

    [HttpGet("drive-types")]
    public async Task<Ok<List<GetDriveTypesResponse>>> GetDriveTypes()
    {
        var items = await context.CarDriveTypes
            .Select(driveType => new GetDriveTypesResponse(driveType.Id, driveType.Name)).ToListAsync();
        return TypedResults.Ok(items);
    }

    [HttpGet("engine-types")]
    public async Task<Ok<List<GetEngineTypesResponse>>> GetEngineTypes()
    {
        var items = await context.CarEngineTypes
            .Select(engineType => new GetEngineTypesResponse(engineType.Id, engineType.Name)).ToListAsync();
        return TypedResults.Ok(items);
    }

    [HttpGet("brand-items/{brandId}/models/{modelId}/generations")]
    public async Task<Ok<List<GetGenerationTypesResponse>>> GetGenerations(int brandId, int modelId)
    {
        var generations = await context.CarGenerations
            .Where(carGeneration => carGeneration.CarModelId == modelId)
            .Include(generation => generation.CarSeries)
            .ThenInclude(carSerie => carSerie.CarBodyTypeNavigation)
            .ToListAsync();

        var result = generations.Select(generation =>
        {
            int? yearBegin = int.TryParse(generation.YearBegin, out var parsedYearBegin)
                ? parsedYearBegin
                : default(int?);
            int? yearEnd = int.TryParse(generation.YearEnd, out var parsedYearEnd) ? parsedYearEnd : default(int?);
            return new GetGenerationTypesResponse(
                generation.Id,
                generation.Name,
                yearBegin,
                yearEnd,
                generation.CarSeries.Select(carSerie =>
                    new GetBodyTypesResponse(carSerie.Id, carSerie.CarBodyTypeNavigation.Name)).ToList()
            );
        }).ToList();

        return TypedResults.Ok(result);
    }

    [HttpGet("interior/colors")]
    public async Task<Ok<List<GetCarInteriorColorsResponse>>> GetCarInteriorColors()
    {
        var result = await context.CarInteriorColors
            .Select(interiorColor => new GetCarInteriorColorsResponse(interiorColor.Id, interiorColor.Name))
            .ToListAsync();

        return TypedResults.Ok(result);
    }

    [HttpGet("interior/materials")]
    public async Task<Ok<List<GetCarInteriorMaterialsResponse>>> GetCarInteriorMaterials()
    {
        var result = await context.CarInteriorMaterials
            .Select(interiorColor => new GetCarInteriorMaterialsResponse(interiorColor.Id, interiorColor.Name))
            .ToListAsync();

        return TypedResults.Ok(result);
    }

    [HttpGet("generations/{generationId}/modifications")]
    public async Task<Ok<List<GetGenerationModificationsResponse>>> GetGenerationModifications(int generationId)
    {
        var modifications = await context.CarModifications
            .Include(m => m.CarSerieNavigation)
                .ThenInclude(cs => cs.CarBodyTypeNavigation)
            .Include(m => m.CarDriveTypeNavigation)
            .Include(m => m.CarEngineTypeNavigation)
            .Include(m => m.CarTransmissionTypeNavigation)
            .Where(m => m.CarSerieNavigation.CarGenerationId == generationId)
            .ToListAsync();

        var result = modifications.Select(m => new GetGenerationModificationsResponse(
            m.Id,
            m.Name,
            new GetBodyTypesResponse(
                m.CarSerieNavigation.CarBodyTypeId,
                m.CarSerieNavigation.CarBodyTypeNavigation.Name
            ),
            m.CarDriveTypeNavigation != null
                ? new GetDriveTypesResponse(
                    m.CarDriveTypeId ?? 0,
                    m.CarDriveTypeNavigation.Name
                )
                : null,
            m.CarTransmissionTypeNavigation != null
                ? new GetTransmissionTypesResponse(
                    m.CarTransmissionTypeId ?? 0,
                    m.CarTransmissionTypeNavigation.Name
                )
                : null,
            m.CarEngineTypeNavigation != null
                ? new GetEngineTypesResponse(
                    m.CarEngineTypeId ?? 0,
                    m.CarEngineTypeNavigation.Name
                )
                : null
        )).ToList();

        return TypedResults.Ok(result);
    }

    [HttpGet("transmission-types")]
    public async Task<Ok<List<GetTransmissionTypesResponse>>> GetTransmissionTypes()
    {
        var result = await context.CarTransmissionTypes
            .Select(transmissionType => new GetTransmissionTypesResponse(transmissionType.Id, transmissionType.Name))
            .ToListAsync();
        return TypedResults.Ok(result);
    }

    [HttpGet("exchange/types")]
    public async Task<Ok<List<GetExchangeTypesResponse>>> GetExchangeTypes()
    {
        var result = await context.CarExchangeOptions
            .Select(exchangeOption => new GetExchangeTypesResponse(exchangeOption.Id, exchangeOption.Name))
            .ToListAsync();
        return TypedResults.Ok(result);
    }
    
    [HttpGet("modifications/{modificationId}/characteristics")]
    public async Task<Results<Ok<List<GetCarCharacteristicResponse>>, NotFound>> GetCarModificationCharacteristics(int modificationId)
    {
        var result = await context.CarCharacteristicValues
            .Include(cv => cv.CarCharacteristicNavigation) // Загружаем информацию о характеристиках
            .Where(cv => cv.CarModificationId == modificationId) // Фильтруем по ID модификации
            .ToListAsync();

        if (!result.Any())
        {
            return TypedResults.NotFound();
        }

        var characteristicsResponse = result.Select(cv => new GetCarCharacteristicResponse(
            cv.CarCharacteristicNavigation.Name!,
            cv.Value,
            cv.Unit
        )).ToList();

        return TypedResults.Ok(characteristicsResponse);
    }
}
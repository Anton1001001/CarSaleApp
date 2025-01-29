using Car.API.DTO;
using Car.API.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Car.API.Controllers;

[ApiController]
[Route("api/[controller]/catalog")]
public class CarsController(CarInfoDbContext context) : ControllerBase
{

    // [HttpGet("body-types")]
    // public async Task<Ok<List<GetAllBodyTypesResponse>>> GetBodyTypes()
    // {
    //     // var items = await context.CarSeries.GroupBy(cs => cs.Name)
    //     //     .Select(g => new GetAllBodyTypesResponse(g.First().Id, g.Key)) 
    //     //     .ToListAsync(); 
    //     // return TypedResults.Ok(items);
    // }
    [HttpGet("brand-items")]
    public async Task<Ok<List<GetAllBrandsResponse>>> GetBrands()
    {
        var items = await context.CarBrands
            .Select(carBrand => new GetAllBrandsResponse(carBrand.Id, carBrand.Name))
            .ToListAsync();
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

    /*[HttpGet("drive-types")]
    public async Task<Ok<List<GetDriveTypesResponse>>> GetDriveTypes()
    {
        var items = await context.CarCharacteristicValues.Where(carCharacteristicValue => carCharacteristicValue.)
    }*/
    
    


}

/*public record GetDriveTypesResponse*/


public record GetCarConditionsResponse(int Id, string Name);
public record GetCarColorsResponse(int Id, string Name);

public record GetCarModelsByBrandResponse(int Id, string Name);

public record GetAllBodyTypesResponse(int Id, string Name);
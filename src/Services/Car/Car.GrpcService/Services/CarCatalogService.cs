using Car.Application.Queries.BodyType.GetBodyTypeById;
using Car.Application.Queries.Brand.GetBrandById;
using Car.Application.Queries.Brand.GetBrands;
using Car.Application.Queries.Color.GetColorById;
using Car.Application.Queries.Color.GetColors;
using Car.Application.Queries.Condition.GetConditionById;
using Car.Application.Queries.DriveType.GetDriveTypeById;
using Car.Application.Queries.EngineType.GetEngineTypeById;
using Car.Application.Queries.Generation.GetGenerationById;
using Car.Application.Queries.Generation.GetModelGenerations;
using Car.Application.Queries.InteriorColor.GetInteriorColorById;
using Car.Application.Queries.InteriorColor.GetInteriorColors;
using Car.Application.Queries.InteriorMaterial.GetInteriorMaterialById;
using Car.Application.Queries.InteriorMaterial.GetInteriorMaterials;
using Car.Application.Queries.Model.GetBrandModels;
using Car.Application.Queries.Model.GetModelById;
using Car.Application.Queries.Modification.GetGenerationModifications;
using Car.Application.Queries.Modification.GetModificationById;
using Car.Application.Queries.TransmissionType.GetTransmissionTypeById;
using Car.Application.Queries.Year.GetModelYears;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace Car.GrpcService.Services;

public class CarCatalogService(ISender sender, ILogger<CarCatalogService> logger) : CarCatalog.CarCatalogBase
{
    public override async Task<GetModelYearsResponse> GetModelYears(GetModelYearsRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("pass into CarCatalogService.GetModelYears");
        var years = await sender.Send(new GetModelYearsQuery { ModelId = request.ModelId }, context.CancellationToken);
        var response = new GetModelYearsResponse();
        var yearsResponse = years
            .Select(year => new Year
            {
                Year_ = year.Year
            })
            .ToList();
        response.Years.AddRange(yearsResponse);
        return response;
    }

    public override async Task<GetInteriorMaterialsResponse> GetInteriorMaterials(Empty request,
        ServerCallContext context)
    {
        var interiorMaterials = await sender.Send(new GetInteriorMaterialsQuery(), context.CancellationToken);
        var response = new GetInteriorMaterialsResponse();
        var interiorMaterialsResponse = interiorMaterials
            .Select(interiorMaterial => new InteriorMaterial
            {
                Id = interiorMaterial.Id,
                Name = interiorMaterial.Name,
            })
            .ToList();
        response.InteriorMaterials.AddRange(interiorMaterialsResponse);
        return response;
    }

    public override async Task<GetInteriorColorsResponse> GetInteriorColors(Empty request, ServerCallContext context)
    {
        var interiorColors = await sender.Send(new GetInteriorColorsQuery(), context.CancellationToken);
        var response = new GetInteriorColorsResponse();
        var interiorColorsResponse = interiorColors
            .Select(interiorColor => new InteriorColor
            {
                Id = interiorColor.Id,
                Name = interiorColor.Name,
            }).ToList();
        response.InteriorColors.AddRange(interiorColorsResponse);
        return response;
    }

    public override async Task<GetColorsResponse> GetColors(Empty request, ServerCallContext context)
    {
        var colors = await sender.Send(new GetColorsQuery(), context.CancellationToken);
        var response = new GetColorsResponse();
        var colorsResponse = colors
            .Select(color =>
                new Color
                {
                    Id = color.Id, Name = color.Name
                })
            .ToList();
        response.Colors.AddRange(colorsResponse);
        return response;
    }

    public override async Task<GetGenerationModificationsResponse> GetGenerationModifications(
        GetGenerationModificationsRequest request, ServerCallContext context)

    {
        var modifications = await sender.Send(new GetGenerationModificationsQuery(request.GenerationId),
            context.CancellationToken);
        var grpcModifications = modifications.Select(modification =>
            new Modification
            {
                Id = modification.Id,
                Name = modification.Name,
                BodyType = new BodyType { Id = modification.BodyType.Id, Name = modification.BodyType.Name },
                DriveType = new DriveType { Id = modification.DriveType!.Id, Name = modification.DriveType.Name },
                TransmissionType = new TransmissionType
                    { Id = modification.TransmissionType!.Id, Name = modification.TransmissionType.Name },
                EngineType = new EngineType { Id = modification.EngineType!.Id, Name = modification.EngineType.Name },
            }).ToList();
        var response = new GetGenerationModificationsResponse();
        response.Modifications.AddRange(grpcModifications);
        return response;
    }


    public override async Task<GetModelGenerationsResponse> GetModelGenerations(
        GetModelGenerationsRequest request,
        ServerCallContext context)
    {
        var generations = await sender.Send(
            new GetModelGenerationsQuery(request.ModelId, request.Year),
            context.CancellationToken
        );

        var response = new GetModelGenerationsResponse
        {
            Generations =
            {
                generations.Select(generation => new Generation
                {
                    Id = generation.Id,
                    Name = generation.Name,
                    YearBegin = generation.YearBegin,
                    YearEnd = generation.YearEnd ?? DateTime.Now.Year,
                    BodyTypes =
                    {
                        generation.CarBodyTypes.Select(bodyType => new BodyType
                        {
                            Id = bodyType.Id,
                            Name = bodyType.Name
                        })
                    }
                })
            }
        };

        return response;
    }


    public override async Task<GetBrandModelsResponse> GetBrandModels(GetBrandModelsRequest request,
        ServerCallContext context)
    {
        var models = await sender.Send(new GetBrandModelsQuery(request.BrandId), context.CancellationToken);
        var grpcModels = models.Select(model => new Model { Id = model.Id, Name = model.Name });
        var response = new GetBrandModelsResponse();
        response.Models.AddRange(grpcModels);
        return response;
    }

    public override async Task<GetBrandsResponse> GetBrands(Empty request, ServerCallContext context)
    {
        var brands = await sender.Send(new GetBrandsQuery(), context.CancellationToken);
        var grpcBrands = brands.Select(brand => new Brand { Id = brand.Id, Name = brand.Name });
        var response = new GetBrandsResponse();
        response.Brands.AddRange(grpcBrands);
        return response;
    }

    public override async Task<GetCarParametersResponse> GetCarParameters(GetCarParametersRequest request, ServerCallContext context)
    {
        var brand = await sender.Send(new GetBrandByIdQuery(request.BrandId), context.CancellationToken);
        
        var model = await sender.Send(new GetModelByIdQuery(request.ModelId), context.CancellationToken);
        
        var generation = await sender.Send(new GetGenerationByIdQuery(request.GenerationId), context.CancellationToken);
        
        var modification = await sender.Send(new GetModificationByIdQuery(request.ModificationId), context.CancellationToken);
        
        var bodyType = await sender.Send(new GetBodyTypeByIdQuery(request.BodyTypeId), context.CancellationToken);
        
        var transmissionType = await sender.Send(new GetTransmissionTypeByIdQuery(request.TransmissionTypeId), context.CancellationToken);
        
        var engineType = await sender.Send(new GetEngineTypeByIdQuery(request.EngineTypeId), context.CancellationToken);
        
        var driveType = await sender.Send(new GetDriveTypeByIdQuery(request.DriveTypeId), context.CancellationToken);
        
        var condition = await sender.Send(new GetConditionByIdQuery(request.ConditionId), context.CancellationToken);
        
        var color = await sender.Send(new GetColorByIdQuery(request.ColorId), context.CancellationToken);
        
        var interiorColor = await sender.Send(new GetInteriorColorByIdQuery(request.InteriorColorId), context.CancellationToken);
        
        var interiorMaterial = await sender.Send(new GetInteriorMaterialByIdQuery(request.InteriorMaterialId), context.CancellationToken);
        
        var response = new GetCarParametersResponse
        {
            Brand = brand.Name,
            Model = model.Name,
            Generation = generation.Name,
            Modification = modification.Name,
            BodyType = bodyType.Name,
            TransmissionType = transmissionType.Name,
            EngineType = engineType.Name,
            DriveType = driveType.Name,
            Condition = condition.Name,
            Color = color.Name,
            InteriorColor = interiorColor.Name,
            InteriorMaterial = interiorMaterial.Name,
        };

        if (modification.GroundClearance is not null)
        {
            response.GroundClearance = (float)(modification.GroundClearance.Value);
        }

        if (modification.EnginePower is not null)
        {
            response.EnginePower = modification.EnginePower.Value;
        }

        if (modification.FuelConsumptionCombined is not null)
        {
            response.FuelConsumptionCombined = (float)(modification.FuelConsumptionCombined.Value);
        }

        if (modification.EngineCapacity is not null)
        {
            response.EngineCapacity = modification.EngineCapacity.Value;
        }
        
        return response;
    }
    

    public override async Task<GetCarParametersPreviewResponse> GetCarParametersPreview(
        GetCarParametersPreviewRequest request, ServerCallContext context)
    {
        var brand = await sender.Send(new GetBrandByIdQuery(request.BrandId), context.CancellationToken);

        var model = await sender.Send(new GetModelByIdQuery(request.ModelId), context.CancellationToken);

        var generation = await sender.Send(new GetGenerationByIdQuery(request.GenerationId), context.CancellationToken);

        return new GetCarParametersPreviewResponse()
        {
            Brand = brand.Name,
            Model = model.Name,
            Generation = generation.Name
        };
    }
}
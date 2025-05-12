using Advert.Application.Abstractions.GrpcClients;
using Advert.Application.Common;
using Advert.Application.Common.Advert.Models.Parameters;
using Advert.Application.CQRS.Queries.GetAdvertForm.Models;
using Advert.Application.Errors;
using Advert.Domain.Constants;
using Advert.Domain.Entities;
using Advert.Domain.Interfaces.Repositories;
using AutoMapper;
using FluentResults;
using Microsoft.Extensions.Logging;
using static Advert.Application.Helpers.ListUtils;

namespace Advert.Application.CQRS.Queries.GetAdvertForm.Processors.GetCarAdvertForm;

public class GetCarAdvertFormProcessor(
    IUnitOfWork unitOfWork,
    ICarCatalogGrpcClient carCatalogGrpcClient,
    IMapper mapper,
    ILogger<GetCarAdvertFormProcessor> logger)
    : Processor<GetAdvertFormQuery, Result<GetAdvertFormResponse>>
{
    protected override bool CanHandle(GetAdvertFormQuery request)
    {
        return request.Parameters is CarsParameters;
    }

    protected override async Task<Result<GetAdvertFormResponse>> ProcessAsync(GetAdvertFormQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Parameters is not CarsParameters carsParameters)
        {
            return new AdvertBadRequestError();
        }
        
        logger.LogInformation($"Place Region: {carsParameters.PlaceRegionId}");

        var placeRegions = await unitOfWork.PlaceRepository.GetPlacesByTypeAsync(PlaceTypes.Region, cancellationToken);
        var phoneCodes = await unitOfWork.Repository<PhoneCode>().GetAllAsync(cancellationToken);

        var placeRegionsResponse = mapper.Map<List<PlaceRegionResponse>>(placeRegions);
        var phoneCodeResponse = mapper.Map<List<PhoneCodeResponse>>(phoneCodes);
        

        var response = new GetAdvertFormResponse(
            Brands: await carCatalogGrpcClient.GetBrandsAsync(cancellationToken),
            Colors: await carCatalogGrpcClient.GetColorsAsync(cancellationToken),
            InteriorColors: await carCatalogGrpcClient.GetInteriorColorsAsync(cancellationToken),
            InteriorMaterials: await carCatalogGrpcClient.GetInteriorMaterialsAsync(cancellationToken),
            PlaceRegions: placeRegionsResponse,
            PhoneCodes: phoneCodeResponse
        );

        if (carsParameters.PlaceRegionId is not null)
        {
            var placeCities = await unitOfWork.PlaceRepository
                .GetPlacesByParentIdAsync(carsParameters.PlaceRegionId.Value, cancellationToken);

            response = response with { PlaceCities = mapper.Map<List<PlaceCityResponse>>(placeCities) };
        }

        if (carsParameters.BrandId is not null)
            response = response with
            {
                Models = await carCatalogGrpcClient
                    .GetBrandModelsAsync(carsParameters.BrandId.Value, cancellationToken)
            };

        if (carsParameters.ModelId is not null)
            response = response with
            {
                Years = await carCatalogGrpcClient
                    .GetModelYearsAsync(carsParameters.ModelId.Value, cancellationToken)
            };

        if (carsParameters.ModelId is not null && carsParameters.Year is not null)
            response = response with
            {
                Generations = await carCatalogGrpcClient
                    .GetModelGenerationsAsync(carsParameters.ModelId.Value, cancellationToken, carsParameters.Year.Value)
            };

        if (carsParameters.GenerationId is null)
            return response;

        var modifications = await carCatalogGrpcClient
            .GetGenerationModificationsAsync(carsParameters.GenerationId.Value, cancellationToken);

        if (response.Generations is not null)
        {
            var selectedGeneration = response.Generations.FirstOrDefault(g => g.Id == carsParameters.GenerationId);
            response = response with { BodyTypes = selectedGeneration?.BodyTypes };
        }

        modifications = FilterList(
            modifications,
            carsParameters.BodyTypeId,
            m => m.BodyType.Id);

        response = response with
        {
            TransmissionTypes = GetDistinctValues(
                modifications,
                m => m.TransmissionType,
                t => t.Id)
        };

        modifications = FilterList(
            modifications,
            carsParameters.TransmissionTypeId,
            m => m.TransmissionType.Id);

        response = response with
        {
            EngineTypes = GetDistinctValues(
                modifications,
                m => m.EngineType,
                e => e.Id)
        };

        modifications = FilterList(
            modifications, 
            carsParameters.EngineTypeId, 
            m => m.EngineType.Id);

        response = response with
        {
            DriveTypes = GetDistinctValues(
                modifications,
                m => m.DriveType,
                d => d.Id)
        };

        if (carsParameters.DriveTypeId is not null)
            response = response with
            {
                Modifications = FilterList(
                    modifications, 
                    carsParameters.DriveTypeId, 
                    m => m.DriveType.Id)
            };
        
        return response;
    }
}
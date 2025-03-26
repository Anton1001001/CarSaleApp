using Advert.Application.Abstractions;
using Advert.Application.Abstractions.GrpcClients;
using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Cars.Models;
using Advert.Application.Errors;
using Advert.Application.Errors.Base;
using Advert.Domain.Constants;
using AutoMapper;
using FluentResults;
using Newtonsoft.Json;

namespace Advert.Application.Services.Processors.GetCarAdvertById;

public class GetCarAdvertByIdProcessor(
    IFileServiceGrpcClient fileServiceGrpcClient,
    ICarCatalogGrpcClient carCatalogGrpcClient,
    IMapper mapper,
    ICurrencyConverter currencyConverter) : Processor<Domain.Entities.Advert, Result<AdvertResponse>>
{
    protected override bool CanHandle(Domain.Entities.Advert request)
    {
        return request.AdvertType == AdvertType.Cars;
    }

    protected override async Task<Result<AdvertResponse>> ProcessAsync(
        Domain.Entities.Advert request,
        CancellationToken cancellationToken)
    {
        var carParameters = JsonConvert.DeserializeObject<CarParametersToSave>(request.Properties);

        if (carParameters is null)
        {
            return new InternalServerError(code: "CarAdvert.GetById", message: "Car parameters error");
        }
        
        var carsCatalogRequest = mapper.Map<CarsCatalogRequest>(carParameters);
        var carCatalogResponse = await carCatalogGrpcClient.GetCarParametersAsync(carsCatalogRequest, cancellationToken);

        var carParametersToShow = mapper.Map<CarParametersResponse>(carCatalogResponse);

        carParametersToShow = carParametersToShow with
        {
            MileageKm = carParameters.MileageKm,
            Year = carParameters.Year,
            Registration = carParameters.RegistrationStatus == Domain.Enums.RegistrationStatus.Unregistered
                ? RegistrationStatus.Unregistered
                : null
        };

        var priceResponse = await currencyConverter
            .ConvertPriceToAllCurrenciesAsync(request.PriceAmount, request.PriceCurrency, cancellationToken);

        var advertResponse = mapper.Map<AdvertResponse>(request);
        advertResponse = advertResponse with
        {
            Parameters = carParametersToShow,
            Price = priceResponse
        };

        var photoRequest = request.AdvertPhotos?
            .Select(photo => photo.FileId)
            .ToList();

        if (photoRequest is not null)
        {
            var photoResponse = await fileServiceGrpcClient
                .GetFilesByIdsResponseAsync(photoRequest, cancellationToken);
            var mainPhotoId = request.AdvertPhotos?
                .FirstOrDefault(photo => photo.IsMain)?.FileId;
            
            if (mainPhotoId.HasValue)
            {
                var index = photoResponse.FindIndex(photo => photo.Id == mainPhotoId.Value);
                if (index != -1)
                {
                    var updatedPhoto = photoResponse[index] with { Main = true };
                    photoResponse[index] = updatedPhoto;
                }
            }
            
            advertResponse.Photos.AddRange(photoResponse);
        }
        
        return advertResponse;
    }
}
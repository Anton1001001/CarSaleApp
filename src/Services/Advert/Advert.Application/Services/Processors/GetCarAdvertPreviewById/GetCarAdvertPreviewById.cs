using Advert.Application.Abstractions;
using Advert.Application.Abstractions.GrpcClients;
using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Cars.Models;
using Advert.Application.Errors.Base;
using Advert.Domain.Constants;
using AutoMapper;
using FluentResults;
using Newtonsoft.Json;

namespace Advert.Application.Services.Processors.GetCarAdvertPreviewById;

public class GetCarAdvertPreviewById(
    IFileServiceGrpcClient fileServiceGrpcClient,
    ICarCatalogGrpcClient carCatalogGrpcClient,
    IMapper mapper,
    ICurrencyConverter currencyConverter) : Processor<Domain.Entities.Advert, Result<AdvertPreviewResponse>>
{
    protected override bool CanHandle(Domain.Entities.Advert request)
    {
        return request.AdvertType == AdvertType.Cars;
    }

    protected override async Task<Result<AdvertPreviewResponse>> ProcessAsync(Domain.Entities.Advert request,
        CancellationToken cancellationToken)
    {
        var carParameters = JsonConvert.DeserializeObject<CarParametersToSave>(request.Properties);

        if (carParameters is null)
        {
            return new InternalServerError(code: "CarAdvert.GetById", message: "Car parameters error");
        }

        var carsCatalogPreviewRequest = mapper.Map<CarsCatalogPreviewRequest>(carParameters);
        var carsCatalogPreviewResponse =
            await carCatalogGrpcClient.GetCarParametersPreviewAsync(carsCatalogPreviewRequest, cancellationToken);

        var priceResponse = await currencyConverter
            .ConvertPriceToAllCurrenciesAsync(request.PriceAmount, request.PriceCurrency, cancellationToken);

        var photoRequest = request.AdvertPhotos?
            .FirstOrDefault(photo => photo.IsMain)?.FileId;
        
        var advertPreviewResponse = mapper.Map<AdvertPreviewResponse>(request);
        advertPreviewResponse = advertPreviewResponse with
        {
            Price = priceResponse,
            Brand = carsCatalogPreviewResponse.Brand,
            Model = carsCatalogPreviewResponse.Model,
            Generation = carsCatalogPreviewResponse.Generation,
            Year = carParameters.Year,
            SellerId = request.SellerId.ToString()
        };
        
        if (photoRequest is not null)
        {
            var photoResponse = await fileServiceGrpcClient
                .GetFilesByIdsResponseAsync([photoRequest.Value], cancellationToken);
            advertPreviewResponse = advertPreviewResponse with
            {
                PhotoPreviewUrl = photoResponse[0].Small.Url
            };
        }

        return advertPreviewResponse;
    }
}
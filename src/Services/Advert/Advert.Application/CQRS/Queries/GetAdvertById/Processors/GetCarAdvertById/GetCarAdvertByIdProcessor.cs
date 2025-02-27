using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Cars;
using Advert.Application.Common.Cars.Models;
using Advert.Application.Interfaces;
using Advert.Application.Services;
using Advert.Domain.Constants;
using AutoMapper;
using Newtonsoft.Json;

namespace Advert.Application.CQRS.Queries.GetAdvertById.Processors.GetCarAdvertById;

public class GetCarAdvertByIdProcessor(
    ICarCatalogGrpcClient carCatalogGrpcClient,
    IMapper mapper,
    ICurrencyConverter currencyConverter) : Processor<Domain.Entities.Advert, AdvertResponse>
{
    protected override bool CanHandle(Domain.Entities.Advert request)
    {
        return request.AdvertType == AdvertType.Cars;
    }

    protected override async Task<AdvertResponse> ProcessAsync(
        Domain.Entities.Advert request,
        CancellationToken cancellationToken)
    {
        var carParameters = JsonConvert.DeserializeObject<CarParametersToSave>(request.Properties);
        var carsCatalogRequest = mapper.Map<CarsCatalogRequest>(carParameters);
        var carCatalogResponse = await carCatalogGrpcClient.GetCarParametersAsync(carsCatalogRequest);

        var carParametersToShow = mapper.Map<CarParametersResponse>(carCatalogResponse);
        carParametersToShow.MileageKm = carParameters.MileageKm;
        carParametersToShow.Year = carParameters.Year;
        carParametersToShow.Registration =
            carParameters.RegistrationStatus == Domain.Enums.RegistrationStatus.Unregistered
                ? RegistrationStatus.Unregistered
                : null;

        var priceResponse = await currencyConverter
            .ConvertPriceToAllCurrenciesAsync(request.PriceAmount.Value, request.PriceCurrency.Value);
        
        var advertResponse = mapper.Map<AdvertResponse>(request);
        advertResponse.Parameters = carParametersToShow;
        advertResponse.Price = priceResponse;


        return advertResponse;
    }
}
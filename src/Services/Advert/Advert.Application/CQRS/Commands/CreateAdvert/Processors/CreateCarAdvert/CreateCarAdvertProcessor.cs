using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Advert.Models.Parameters;
using Advert.Application.Common.Cars.Models;
using Advert.Application.Errors;
using Advert.Application.Errors.Base;
using Advert.Application.Helpers;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Constants;
using Advert.Domain.Enums;
using Advert.Domain.Interfaces.Repositories;
using AutoMapper;
using FluentResults;
using Newtonsoft.Json;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateCarAdvert;

public class CreateCarAdvertProcessor(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IAdvertService advertService)
    : Processor<CreateAdvertCommand, Result<AdvertResponse>>
{
    protected override bool CanHandle(CreateAdvertCommand request)
    {
        return request.Parameters is CarsParameters;
    }

    protected override async Task<Result<AdvertResponse>> ProcessAsync(CreateAdvertCommand request,
        CancellationToken cancellationToken)
    {
        var parametersBase = request.Parameters;

        if (parametersBase is not CarsParameters carParameters)
        {
            return new AdvertBadRequestError();
        }

        var carParamsToSave = mapper.Map<CarParametersToSave>(carParameters);
        
        var mileageKm = carParameters.MileageUnit == MileageUnit.Kilometers
            ? carParameters.Mileage
            : DistanceConverter.MilesToKilometers(carParameters.Mileage);

        carParamsToSave = carParamsToSave with { MileageKm = mileageKm };

        var carParamsJson = JsonConvert.SerializeObject(carParamsToSave, Formatting.Indented);

        var advert = mapper.Map<Domain.Entities.Advert>(parametersBase);
        advert.Properties = carParamsJson;

        advert.AdvertStatus = AdvertStatus.Active;

        var advertPrivateStatus = await unitOfWork.AdvertPrivateStatusRepository
            .GetByNameAsync(AdvertPrivateStatus.Active, cancellationToken);

        if (advertPrivateStatus is null)
        {
            return new InternalServerError(code: "Advert.PrivateStatus", message: "Private status error");
        }

        advert.AdvertPrivateStatus = advertPrivateStatus;

        var advertPublicStatus = await unitOfWork.AdvertPublicStatusRepository
            .GetByNameAsync(AdvertPublicStatus.Active, cancellationToken);

        if (advertPublicStatus is null)
        {
            return new InternalServerError(code: "Advert.PublicStatus", message: "Public status error");
        }

        advert.AdvertPublicStatus = advertPublicStatus;

        var advertEntity = await unitOfWork.AdvertRepository.CreateAsync(advert, cancellationToken);

        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (!saveResult)
        {
            return new InternalServerError(code: "CarAdvert.Create", message: "Failed to save data");
        }

        var advertResponse = await advertService.GetAdvertByIdAsync(advertEntity.Id, cancellationToken);

        return advertResponse;
    }
}
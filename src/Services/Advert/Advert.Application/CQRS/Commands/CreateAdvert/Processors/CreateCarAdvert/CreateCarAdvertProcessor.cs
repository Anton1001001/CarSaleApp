using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Cars;
using Advert.Application.Common.Cars.Models;
using Advert.Application.CQRS.Commands.CreateAdvert.Parameters;
using Advert.Application.CQRS.Queries.GetAdvertById;
using Advert.Application.Helpers;
using Advert.Application.Interfaces;
using Advert.Application.Services;
using Advert.Domain.Constants;
using Advert.Domain.Enums;
using Advert.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using RegistrationStatus = Advert.Domain.Enums.RegistrationStatus;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateCarAdvert;

public class CreateCarAdvertProcessor(
    IMapper mapper, 
    IUnitOfWork unitOfWork, 
    ISender sender) : Processor<CreateAdvertCommand, AdvertResponse>
{
    protected override bool CanHandle(CreateAdvertCommand request)
    {
        return request.Parameters is CarsParameters;
    }

    protected override async Task<AdvertResponse> ProcessAsync(CreateAdvertCommand request,
        CancellationToken cancellationToken)
    {
        var paramsBase = request.Parameters;
        var carParams = paramsBase as CarsParameters;

        var carParamsToSave = mapper.Map<CarParametersToSave>(carParams);

        carParamsToSave.MileageKm = carParams!.MileageUnit == MileageUnit.Kilometers
            ? carParams.Mileage
            : DistanceConverter.MilesToKilometers(carParams.Mileage);

        var carParamsJson = JsonConvert.SerializeObject(carParamsToSave, Formatting.Indented);

        var advert = mapper.Map<Domain.Entities.Advert>(paramsBase);
        advert.Properties = carParamsJson;

        advert.AdvertStatus = AdvertStatus.Active;

        advert.AdvertPrivateStatus = await unitOfWork.AdvertPrivateStatusRepository
            .GetByNameAsync(AdvertPrivateStatus.Active, cancellationToken);

        advert.AdvertPublicStatus = await unitOfWork.AdvertPublicStatusRepository
            .GetByNameAsync(AdvertPublicStatus.Active, cancellationToken);

        var advertEntity = await unitOfWork.AdvertRepository.CreateAsync(advert, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var advertResponse = await sender.Send(new GetAdvertByIdQuery(advertEntity.Id), cancellationToken);
        
        
        return advertResponse;
    }
}
using Advert.Application.Common.Advert.Models;
using Advert.Application.Errors;
using Advert.Application.Errors.Base;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Constants;
using Advert.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.PauseAdvert;

public class PauseAdvertHandler(
    IUnitOfWork unitOfWork,
    IAdvertService advertService)
    : IRequestHandler<PauseAdvertCommand, Result<AdvertResponse>>
{
    public async Task<Result<AdvertResponse>> Handle(PauseAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);

        if (advert is null)
        {
            return new AdvertNotFoundError(message: $"Advert with id: {request.Id} was not found");
        }

        advert.AdvertStatus = AdvertStatus.Paused;

        var advertPublicStatus = await unitOfWork.AdvertPublicStatusRepository.GetByNameAsync(
            AdvertPublicStatus.TemporaryUnavailable,
            cancellationToken);
        
        if (advertPublicStatus is null)
        {
            return new InternalServerError(code: "Advert.Pause", message: "Public status error");
        }

        advert.AdvertPublicStatus = advertPublicStatus;
        
        var advertPrivateStatus = await unitOfWork.AdvertPrivateStatusRepository.GetByNameAsync(
            AdvertPrivateStatus.Paused,
            cancellationToken);
        
        if (advertPrivateStatus is null)
        {
            return new InternalServerError(code: "Advert.Pause", message: "Private status error");
        }

        advert.AdvertPrivateStatus = advertPrivateStatus;

        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (!saveResult)
        {
            return new InternalServerError(code: "CarAdvert.Pause", message: "Failed to save data");
        }

        var advertResponse = await advertService.GetAdvertByIdAsync(advert.Id, cancellationToken);

        return advertResponse;
    }
}
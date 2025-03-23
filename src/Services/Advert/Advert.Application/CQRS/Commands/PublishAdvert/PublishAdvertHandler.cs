using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Queries.GetAdvertById;
using Advert.Application.Errors;
using Advert.Application.Errors.Base;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Constants;
using Advert.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.PublishAdvert;

public class PublishAdvertHandler(
    IUnitOfWork unitOfWork,
    IAdvertService advertService)
    : IRequestHandler<PublishAdvertCommand, Result<AdvertResponse>>
{
    public async Task<Result<AdvertResponse>> Handle(PublishAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);

        if (advert is null)
        {
            return new AdvertNotFoundError(message: $"Advert with Id: {request.Id} was not found");
        }

        advert.AdvertStatus = AdvertPublicStatus.Active;

        var advertPublicStatus = await unitOfWork.AdvertPublicStatusRepository.GetByNameAsync(
            AdvertPublicStatus.Active, 
            cancellationToken);
        
        if (advertPublicStatus is null)
        {
            return new InternalServerError(code: "Advert.Publish", message: "Public status error");
        }

        advert.AdvertPublicStatus = advertPublicStatus;

        var advertPrivateStatus = await unitOfWork.AdvertPrivateStatusRepository.GetByNameAsync(
            AdvertPrivateStatus.Active,
            cancellationToken);

        if (advertPrivateStatus is null)
        {
            return new InternalServerError(code: "Advert.Publish", message: "Private status error");
        }
        
        advert.AdvertPrivateStatus = advertPrivateStatus;

        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (!saveResult)
        {
            return new InternalServerError(code: "Advert.Publish", message: "Failed to save data");
        }

        var advertResponse = await advertService.GetAdvertByIdAsync(advert.Id, cancellationToken);

        if (advertResponse is null)
        {
            return new AdvertNotFoundError(message: $"Advert with id: {advert.Id} was not found");
        }

        return advertResponse;
    }
}
using Advert.Application.Common.Advert.Models;
using Advert.Application.Errors;
using Advert.Application.Errors.Base;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Constants;
using Advert.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.RemoveAdvert;

public class RemoveAdvertHandler(
    IUnitOfWork unitOfWork, 
    IAdvertService advertService)
    : IRequestHandler<RemoveAdvertCommand, Result<AdvertResponse>>
{
    public async Task<Result<AdvertResponse>> Handle(RemoveAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);

        if (advert is null)
        {
            return new AdvertNotFoundError(message: $"Advert with id: {request.Id} was not found");
        }

        var status = AdvertStatus.Removed;
        var publicStatus = AdvertPublicStatus.Removed;
        var privateStatus = AdvertPrivateStatus.Removed;

        if (request.RemoveReason == AdvertRemoveReason.Sold)
        {
            publicStatus = AdvertPublicStatus.Sold;
            privateStatus = AdvertPrivateStatus.Sold;
        }

        advert.AdvertStatus = status;
        
        var advertPublicStatus = await unitOfWork.AdvertPublicStatusRepository.GetByNameAsync(publicStatus, cancellationToken);
        
        if (advertPublicStatus is null)
        {
            return new InternalServerError(code: "Advert.Remove", message: "Public status error");
        }
        
        advert.AdvertPublicStatus = advertPublicStatus;
            
        var advertPrivateStatus = await unitOfWork.AdvertPrivateStatusRepository.GetByNameAsync(privateStatus, cancellationToken);
        
        if (advertPrivateStatus is null)
        {
            return new InternalServerError(code: "Advert.Remove", message: "Private status error");
        }
        
        advert.AdvertPrivateStatus = advertPrivateStatus;
            
        advert.RemoveReason = request.RemoveReason;

        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (!saveResult)
        {
            return new InternalServerError(code: "Advert.Remove", message: "Failed to save data");
        }

        var advertResponse = await advertService.GetAdvertByIdAsync(advert.Id, cancellationToken);

        return advertResponse;
    }
}
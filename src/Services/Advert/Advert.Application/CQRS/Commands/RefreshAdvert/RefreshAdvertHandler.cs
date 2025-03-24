using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Queries.GetAdvertById;
using Advert.Application.Errors;
using Advert.Application.Errors.Base;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.RefreshAdvert;

public class RefreshAdvertHandler(IUnitOfWork unitOfWork, IAdvertService advertService) 
    : IRequestHandler<RefreshAdvertCommand, Result<AdvertResponse>>
{
    public async Task<Result<AdvertResponse>> Handle(RefreshAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);

        if (advert is null)
        {
            return new AdvertNotFoundError(message: $"Advert with id: {request.Id} was not found");
        }
        
        advert.Refresh();
        
        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (!saveResult)
        {
            return new InternalServerError(code: "Advert.Refresh", message: "Failed to save data");
        }

        var advertResponse = await advertService.GetAdvertByIdAsync(advert.Id, cancellationToken);

        return advertResponse;
    }
}
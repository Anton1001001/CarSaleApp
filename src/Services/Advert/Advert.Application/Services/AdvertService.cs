using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.Errors;
using Advert.Application.Errors.Base;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using FluentResults;

namespace Advert.Application.Services;

public class AdvertService(IUnitOfWork unitOfWork, Processor<Domain.Entities.Advert, Result<AdvertResponse>> processor)
    : IAdvertService
{
    public async Task<Result<AdvertResponse>> GetAdvertByIdAsync(int id, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(id, cancellationToken);

        if (advert is null)
        {
            return new AdvertNotFoundError(message: $"Advert with  id: {id} was not found");
        }

        var response = await processor.HandleAsync(advert, cancellationToken);

        if (response is null)
        {
            return new InternalServerError(code: "Advert.GetById", message: "No processor for supplied vehicle type");
        }

        return response;
    }
}
using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Queries.GetAdvertById;
using Advert.Domain.Constants;
using Advert.Domain.Interfaces;
using MediatR;

namespace Advert.Application.CQRS.Commands.RemoveAdvert;

public class RemoveAdvertHandler(IUnitOfWork unitOfWork, ISender sender)
    : IRequestHandler<RemoveAdvertCommand, AdvertResponse>
{
    public async Task<AdvertResponse> Handle(RemoveAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);

        if (advert == null)
        {
            throw new Exception("Advert not found");
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
        advert.AdvertPublicStatus =
            await unitOfWork.AdvertPublicStatusRepository.GetByNameAsync(publicStatus, cancellationToken);
        advert.AdvertPrivateStatus =
            await unitOfWork.AdvertPrivateStatusRepository.GetByNameAsync(privateStatus, cancellationToken);
        advert.RemoveReason = request.RemoveReason;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var advertResponse = await sender.Send(new GetAdvertByIdQuery(advert.Id), cancellationToken);

        return advertResponse;
    }
}
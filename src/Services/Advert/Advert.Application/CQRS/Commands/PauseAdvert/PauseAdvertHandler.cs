using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Queries.GetAdvertById;
using Advert.Domain.Constants;
using Advert.Domain.Interfaces;
using MediatR;

namespace Advert.Application.CQRS.Commands.PauseAdvert;

public class PauseAdvertHandler(IUnitOfWork unitOfWork, ISender sender)
    : IRequestHandler<PauseAdvertCommand, AdvertResponse>
{
    public async Task<AdvertResponse> Handle(PauseAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);

        advert.AdvertStatus = AdvertStatus.Paused;
        advert.AdvertPublicStatus =
            await unitOfWork.AdvertPublicStatusRepository.GetByNameAsync(AdvertPublicStatus.TemporaryUnavailable,
                cancellationToken);
        advert.AdvertPrivateStatus =
            await unitOfWork.AdvertPrivateStatusRepository.GetByNameAsync(AdvertPrivateStatus.Paused,
                cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        var advertResponse = await sender.Send(new GetAdvertByIdQuery(advert.Id), cancellationToken);
        return advertResponse;
    }
}
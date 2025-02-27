using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Queries.GetAdvertById;
using Advert.Domain.Constants;
using Advert.Domain.Interfaces;
using MediatR;

namespace Advert.Application.CQRS.Commands.PublishAdvert;

public class PublishAdvertHandler(IUnitOfWork unitOfWork, ISender sender)
    : IRequestHandler<PublishAdvertCommand, AdvertResponse>
{
    public async Task<AdvertResponse> Handle(PublishAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);

        advert.AdvertStatus = AdvertPublicStatus.Active;
        advert.AdvertPublicStatus =
            await unitOfWork.AdvertPublicStatusRepository.GetByNameAsync(AdvertPublicStatus.Active, cancellationToken);
        advert.AdvertPrivateStatus =
            await unitOfWork.AdvertPrivateStatusRepository.GetByNameAsync(AdvertPrivateStatus.Active,
                cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        var advertResponse = await sender.Send(new GetAdvertByIdQuery(advert.Id), cancellationToken);
        return advertResponse;
    }
}
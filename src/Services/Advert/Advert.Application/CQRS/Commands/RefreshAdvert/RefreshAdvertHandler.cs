using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Queries.GetAdvertById;
using Advert.Domain.Interfaces;
using MediatR;

namespace Advert.Application.CQRS.Commands.RefreshAdvert;

public class RefreshAdvertHandler(IUnitOfWork unitOfWork, ISender sender) : IRequestHandler<RefreshAdvertCommand, AdvertResponse>
{
    public async Task<AdvertResponse> Handle(RefreshAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);
        
        advert.RefreshedAt = DateTime.Now;
        advert.NextRefreshAvailableAt = DateTime.Now.AddHours(12);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var advertResponse = await sender.Send(new GetAdvertByIdQuery(advert.Id), cancellationToken);

        return advertResponse;
    }
}
using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using MediatR;

namespace Advert.Application.CQRS.Commands.CreateAdvert;

public class CreateAdvertHandler(
    Processor<CreateAdvertCommand, AdvertResponse> processor)
    : IRequestHandler<CreateAdvertCommand, AdvertResponse>
{
    public async Task<AdvertResponse> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        var response = await processor.HandleAsync(request, cancellationToken);
        return response;
    }
}


//Composition root


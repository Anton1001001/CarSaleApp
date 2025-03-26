using System.Threading;
using System.Threading.Tasks;
using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.Errors.Base;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.CreateAdvert;

public class CreateAdvertHandler(
    Processor<CreateAdvertCommand, Result<AdvertResponse>> processor)
    : IRequestHandler<CreateAdvertCommand, Result<AdvertResponse>>
{
    public async Task<Result<AdvertResponse>> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        var response = await processor.HandleAsync(request, cancellationToken);

        if (response is null)
        {
            return new InternalServerError(code: "Advert.Create", message: "No processor for supplied vehicle type");
        }
        
        return response;
    }
}


//Composition root


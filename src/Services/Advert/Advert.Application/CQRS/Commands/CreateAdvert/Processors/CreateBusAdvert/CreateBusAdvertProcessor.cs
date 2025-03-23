using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using FluentResults;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateBusAdvert;

public class CreateBusAdvertProcessor : Processor<CreateAdvertCommand, Result<AdvertResponse>>
{
    protected override bool CanHandle(CreateAdvertCommand request)
    {
        throw new NotImplementedException();
    }

    protected override Task<Result<AdvertResponse>> ProcessAsync(CreateAdvertCommand request,
        CancellationToken cancellationToken)
    {
        var busParameters = request.Parameters;
        throw new NotImplementedException();
    }
}
using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateMotoAdvert;
public class CreateMotoAdvertProcessor : Processor<CreateAdvertCommand, AdvertResponse>
{
    protected override bool CanHandle(CreateAdvertCommand request)
    {
        throw new NotImplementedException();
    }

    protected override Task<AdvertResponse> ProcessAsync(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        var motParameters = request.Parameters;
        throw new NotImplementedException();
    }
}
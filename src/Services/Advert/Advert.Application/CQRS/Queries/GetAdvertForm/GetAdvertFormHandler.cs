using Advert.Application.Common;
using Advert.Application.Errors.Base;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertForm;

public class GetAdvertFormHandler(Processor<GetAdvertFormQuery, Result<GetAdvertFormResponse>> processor)
    : IRequestHandler<GetAdvertFormQuery, Result<GetAdvertFormResponse>>
{
    public async Task<Result<GetAdvertFormResponse>> Handle(GetAdvertFormQuery request, CancellationToken cancellationToken)
    {
        var response = await processor.HandleAsync(request, cancellationToken);

        if (response is null)
        {
            return new InternalServerError(code: "Advert.GetForm", message: "No processor for supplied vehicle type");
        }
        
        return response;
    }
}
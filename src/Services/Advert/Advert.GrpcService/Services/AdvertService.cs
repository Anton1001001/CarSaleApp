using Advert.Application.CQRS.Queries.GetAdvertsPreviewsByIds;
using Advert.Application.Services.Interfaces;
using AutoMapper;
using Grpc.Core;
using MediatR;

namespace Advert.GrpcService.Services;

public class AdvertService(IAdvertService advertService, IMapper mapper, ISender sender) : Advert.AdvertBase
{
    public override async Task<GetAdvertPreviewByIdResponse> GetAdvertPreviewById(GetAdvertPreviewByIdRequest request, ServerCallContext context)
    {
        var advertPreview = await advertService.GetAdvertPreviewByIdAsync(request.Id, context.CancellationToken);
        var response = new GetAdvertPreviewByIdResponse
        {
            AdvertPreview = mapper.Map<AdvertPreview>(advertPreview.Value)
        };
        return response;
    }

    public override async Task<GetAdvertsPreviewsByIdsResponse> GetAdvertsPreviewsByIds(GetAdvertsPreviewsByIdsRequest request, ServerCallContext context)
    {
        var advertPreviews = await sender
            .Send(new GetAdvertsPreviewsByIdsQuery(request.Ids.ToList()), context.CancellationToken);
        
        var advertsPreviewsResponse = mapper.Map<List<AdvertPreview>>(advertPreviews.Value);
        var response = new GetAdvertsPreviewsByIdsResponse();
        response.AdvertsPreviews.AddRange(advertsPreviewsResponse);
        
        
        return response;
    }
}
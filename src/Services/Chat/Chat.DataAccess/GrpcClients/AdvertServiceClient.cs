using Chat.Core.Abstractions;
using Chat.Core.CQRS.Queries.GetUserDialogs;
using Advert.GrpcService;
using AutoMapper;

namespace Chat.DataAccess.GrpcClients;

public class AdvertServiceClient(Advert.GrpcService.Advert.AdvertClient advertServiceGrpcClient, IMapper mapper)
    : IAdvertServiceClient
{
    public async Task<List<AdvertPreviewResponse>> GetAdvertsPreviewsByIdsAsync(List<int> ids,
        CancellationToken cancellationToken = default)
    {
        var request = new GetAdvertsPreviewsByIdsRequest();
        request.Ids.AddRange(ids);
        
        var grpcResponse = await advertServiceGrpcClient
            .GetAdvertsPreviewsByIdsAsync(request, cancellationToken: cancellationToken);
        
        var response = mapper.Map<List<AdvertPreviewResponse>>(grpcResponse.AdvertsPreviews);

        return response;
    }

    public async Task<AdvertPreviewResponse?> GetAdvertPreviewByIdAsync(int id, CancellationToken cancellationToken)
    {
        var grpcRequest = new GetAdvertPreviewByIdRequest { Id = id };
        
        var grpcResponse = await advertServiceGrpcClient
            .GetAdvertPreviewByIdAsync(grpcRequest, cancellationToken: cancellationToken);

        var response = mapper.Map<AdvertPreviewResponse>(grpcResponse.AdvertPreview);
        
        return response;
    }
}
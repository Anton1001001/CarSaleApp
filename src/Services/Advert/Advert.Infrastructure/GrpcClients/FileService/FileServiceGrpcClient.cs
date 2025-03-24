using Advert.Application.Abstractions.GrpcClients;
using Advert.Application.Common.Advert.Models;
using AutoMapper;
using File.GrpcService;

namespace Advert.Infrastructure.GrpcClients.FileService;

public class FileServiceGrpcClient(File.GrpcService.File.FileClient client, IMapper mapper) : IFileServiceGrpcClient 
{
    public async Task<List<PhotoResponse>> GetFilesByIdsResponseAsync(List<int> ids, CancellationToken cancellationToken)
    {
        var request = new GetFilesByIdsRequest();
        request.Ids.AddRange(ids);
        var photos = await client.GetFilesByIdsAsync(request, cancellationToken: cancellationToken);
        var response = mapper.Map<List<PhotoResponse>>(photos.Files);
        
        return response;
    }
}
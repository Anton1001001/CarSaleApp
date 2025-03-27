using AutoMapper;
using File.Core.CQRS.Queries.GetFileById;
using File.Core.CQRS.Queries.GetFilesByIds;
using Grpc.Core;
using MediatR;

namespace File.GrpcService.Services;

public class FileService(ISender sender, IMapper mapper) : File.FileBase
{
    public override async Task<GetFileByIdResponse> GetFileById(GetFileByIdRequest request, ServerCallContext context)
    {
        var photo = await sender.Send(new GetFileByIdQuery(request.Id), context.CancellationToken);
        var fileResponse = mapper.Map<FileResponse>(photo.Value);
        var response = new GetFileByIdResponse
        {
            File = fileResponse
        };

        return response;
    }

    public override async Task<GetFilesByIdsResponse> GetFilesByIds(GetFilesByIdsRequest request, ServerCallContext context)
    {
        var photos = await sender.Send(new GetFilesByIdsQuery(request.Ids.ToList()), context.CancellationToken);
        var filesResponse = mapper.Map<List<FileResponse>>(photos.Value);
        var response = new GetFilesByIdsResponse();
        response.Files.AddRange(filesResponse);
        return response;
    }
}
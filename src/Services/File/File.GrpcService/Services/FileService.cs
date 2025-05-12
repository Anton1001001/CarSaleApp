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
        var response = await sender.Send(new GetFileByIdQuery(request.Id), context.CancellationToken);

        if (response.IsFailed)
        {
            throw new RpcException(new Status(StatusCode.NotFound, response.Errors[0].Message));
        }
        
        var fileResponse = mapper.Map<FileResponse>(response.Value);
        
        return new GetFileByIdResponse
        {
            File = fileResponse
        };
    }

    public override async Task<GetFilesByIdsResponse> GetFilesByIds(GetFilesByIdsRequest request,
        ServerCallContext context)
    {
        var response = await sender.Send(new GetFilesByIdsQuery(request.Ids.ToList()), context.CancellationToken);

        if (response.IsFailed)
        {
            throw new RpcException(new Status(StatusCode.NotFound, response.Errors[0].Message));
        }
        
        var filesResponse = mapper.Map<List<FileResponse>>(response.Value);
        var result = new GetFilesByIdsResponse();
        result.Files.AddRange(filesResponse);

        return result;
    }
}
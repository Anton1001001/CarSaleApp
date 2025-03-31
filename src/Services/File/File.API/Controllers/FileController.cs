using File.API.Extensions;
using File.Core.CQRS.Commands.RemoveFile;
using File.Core.CQRS.Commands.UploadFile;
using File.Core.CQRS.Queries.GetFileById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace File.API.Controllers;

[ApiController]
[Route("api/files")]
public class FileController(ISender sender) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IResult> UploadFile(IFormFile file, CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new UploadFileCommand(file), cancellationToken);
        
        return response.TryGetResult(Results.Ok);
    }

    [HttpDelete("remove")]
    public async Task<IResult> RemoveFile(List<int> ids, CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new RemoveFileCommand(ids), cancellationToken);
        
        return response.TryGetResult(Results.Ok);
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetFileById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new GetFileByIdQuery(id), cancellationToken);
        
        return response.TryGetResult(Results.Ok);
    }
}
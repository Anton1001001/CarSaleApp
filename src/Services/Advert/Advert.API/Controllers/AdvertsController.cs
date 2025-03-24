using Advert.API.Extensions;
using Advert.Application.Common.Advert.Models.Parameters;
using Advert.Application.CQRS.Commands.CreateAdvert;
using Advert.Application.CQRS.Commands.PauseAdvert;
using Advert.Application.CQRS.Commands.PublishAdvert;
using Advert.Application.CQRS.Commands.RefreshAdvert;
using Advert.Application.CQRS.Commands.RemoveAdvert;
using Advert.Application.CQRS.Queries.GetAdvertById;
using Advert.Application.CQRS.Queries.GetAdvertCategories;
using Advert.Application.CQRS.Queries.GetAdvertForm;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Advert.API.Controllers;

[ApiController]
[Route("api/adverts/")]
public class AdvertsController(ISender sender) : ControllerBase
{
    [HttpGet("categories")]
    public async Task<IResult> GetAdvertCategories(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAdvertCategoriesQuery(), cancellationToken);
        
        return Results.Ok(result);
    }
    [HttpGet("{id:int}")]
    public async Task<IResult> GetAdvertById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetAdvertByIdQuery(id), cancellationToken);
        
        return result.TryGetResult(Results.Ok);
    }

    [HttpPost("{type}/create")]
    public async Task<IResult> CreateAdvert([FromRoute] string type, [FromBody] CreateAdvertRequest request, CancellationToken cancellationToken = default)
    {
        var parameters = ParametersFactory.CreateCommand(type, request.Params.ToString());
        var result = await sender.Send(new CreateAdvertCommand(parameters), cancellationToken);
        
        return result.TryGetResult(Results.Ok);
    }
    
    [HttpPost("{id}/publish")]
    public async Task<IResult> PublishAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new PublishAdvertCommand(id), cancellationToken);
        
        return result.TryGetResult(Results.Ok);
    }
    
    [HttpPost("{id}/pause")]
    public async Task<IResult> PauseAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new PauseAdvertCommand(id), cancellationToken);
        
        return result.TryGetResult(Results.Ok);
    }

    [HttpPost("{id}/refresh")]
    public async Task<IResult> RefreshAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new RefreshAdvertCommand(id), cancellationToken);
        
        return result.TryGetResult(Results.Ok);
    }
    
    [HttpPost("{id}/remove")]
    public async Task<IResult> RemoveAdvert(int id, [FromBody] RemoveAdvertCommand request, CancellationToken cancellationToken = default)
    {
        request = request with { Id = id };
        var result = await sender.Send(request, cancellationToken);
        
        return result.TryGetResult(Results.Ok);
    }
    
    [HttpPost("{type}/form")]
    public async Task<IResult> Form([FromRoute] string type, [FromBody] GetAdvertFormRequest request, CancellationToken cancellationToken = default)
    {
        var parameters = ParametersFactory.CreateCommand(type, request.Params.ToString());
        var result = await sender.Send(new GetAdvertFormQuery(parameters), cancellationToken);
        
        return result.TryGetResult(Results.Ok);
    }
}


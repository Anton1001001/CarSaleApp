using Advert.Application.CQRS.Commands.CreateAdvert;
using Advert.Application.CQRS.Commands.CreateAdvert.Parameters;
using Advert.Application.CQRS.Commands.PauseAdvert;
using Advert.Application.CQRS.Commands.PublishAdvert;
using Advert.Application.CQRS.Commands.RefreshAdvert;
using Advert.Application.CQRS.Commands.RemoveAdvert;
using Advert.Application.CQRS.Queries.GetAdvertById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Advert.API.Controllers;

[ApiController]
[Route("api/adverts/")]
public class AdvertsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAdvertById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetAdvertByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{type}/create")]
    public async Task<IActionResult> CreateAdvert([FromRoute] string type, [FromBody] CreateAdvertRequest request, CancellationToken cancellationToken = default)
    {
        var command = ParametersFactory.CreateCommand(type, request.Params.ToString());
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("{id}/publish")]
    public async Task<IActionResult> PublishAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new PublishAdvertCommand(id), cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("{id}/pause")]
    public async Task<IActionResult> PauseAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new PauseAdvertCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{id}/refresh")]
    public async Task<IActionResult> RefreshAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new RefreshAdvertCommand(id), cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("{id}/remove")]
    public async Task<IActionResult> RemoveAdvert(int id, [FromBody] RemoveAdvertCommand request, CancellationToken cancellationToken = default)
    {
        request = request with { Id = id };
        var result = await sender.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("offer-types/{type}/offer-form")]
    public Task<IAsyncResult> GetAdvertForm()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("offer-types/{type}/offer-form/update")]
    public Task<IActionResult> UpdateForm()
    {
        throw new NotImplementedException();
    }

    [HttpPut("offer-types/{type}/offer-form/draft")]
    public Task<IActionResult> OfferDraft()
    {
        throw new NotImplementedException();
    }

    [HttpPut("offers/{id}")]
    public Task<IActionResult> UpdateAdvert()
    {
        throw new NotImplementedException();
    }

    

}
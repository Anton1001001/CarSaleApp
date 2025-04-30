using System.Security.Claims;
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
using Advert.Application.CQRS.Queries.GetAllAdverts;
using Advert.Infrastructure.Options;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Advert.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/adverts/")]
public class AdvertsController(ISender sender) : ControllerBase
{
    
    [HttpGet("categories")]
    public async Task<IActionResult> GetAdvertCategories(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAdvertCategoriesQuery(), cancellationToken);

        return result.ToActionResult();
    }
    
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAdvertById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetAdvertByIdQuery(id), cancellationToken);

        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAdverts(CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetAllAdvertsQuery(), cancellationToken);
        
        return result.ToActionResult();
    }
    
    [HttpPost("{type}/create")]
    public async Task<IActionResult> CreateAdvert([FromRoute] string type, [FromBody] CreateAdvertRequest request, CancellationToken cancellationToken = default)
    {
        var parameters = ParametersFactory.CreateCommand(type, request.Params.ToString());
        var sellerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await sender.Send(new CreateAdvertCommand(sellerId!, parameters), cancellationToken);

        return result.ToActionResult();
    }
    
    [HttpPost("{id}/publish")]
    public async Task<IActionResult> PublishAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new PublishAdvertCommand(id), cancellationToken);

        return result.ToActionResult();
    }
    
    [HttpPost("{id}/pause")]
    public async Task<IActionResult> PauseAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new PauseAdvertCommand(id), cancellationToken);

        return result.ToActionResult();
    }

    [HttpPost("{id}/refresh")]
    public async Task<IActionResult> RefreshAdvert(int id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new RefreshAdvertCommand(id), cancellationToken);

        return result.ToActionResult();
    }
    
    [HttpPost("{id}/remove")]
    public async Task<IActionResult> RemoveAdvert(int id, [FromBody] RemoveAdvertCommand request, CancellationToken cancellationToken = default)
    {
        request = request with { Id = id };
        var result = await sender.Send(request, cancellationToken);

        return result.ToActionResult();
    }
    
    [HttpPost("{type}/form")]
    public async Task<IActionResult> Form([FromRoute] string type, [FromBody] GetAdvertFormRequest request, CancellationToken cancellationToken = default)
    {
        var parameters = ParametersFactory.CreateCommand(type, request.Params.ToString());
        var result = await sender.Send(new GetAdvertFormQuery(parameters), cancellationToken);

        return result.ToActionResult();
    }
}


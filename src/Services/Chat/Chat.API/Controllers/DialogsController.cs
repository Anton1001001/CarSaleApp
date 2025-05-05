using System.Security.Claims;
using Chat.API.Extensions;
using Chat.API.Hubs;
using Chat.Core.CQRS.Commands.CreateDialog;
using Chat.Core.CQRS.Commands.CreateMessage;
using Chat.Core.CQRS.Queries.CheckDialogByAdvertId;
using Chat.Core.CQRS.Queries.GetDialogMessages;
using Chat.Core.CQRS.Queries.GetUserDialogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace Chat.API.Controllers;

[Authorize]
[ApiController]
[Route("api/chat/dialogs")]
public class DialogsController(ISender sender, IHubContext<ChatHub> chatHubContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserDialogs()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await sender.Send(new GetUserDialogsQuery(userId));
        
        return response.ToActionResult();
    }
    
    [HttpGet("{dialogId:int}/messages")]
    public async Task<IActionResult> GetDialogMessages([FromRoute] int dialogId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var query = new GetDialogMessagesQuery(dialogId, userId);
        var response = await sender.Send(query, cancellationToken);
        
        return response.ToActionResult();
    }
    
    [HttpPost("{dialogId:int}/messages")]
    public async Task<IActionResult> CreateMessage([FromRoute] int dialogId, [FromBody] CreateMessageRequest request,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var command = new CreateMessageCommand(userId, dialogId, request.Text);
        var response = await sender.Send(command, cancellationToken);

        await chatHubContext.Clients.Group(dialogId.ToString())
            .SendAsync("ReceiveMessage", response, cancellationToken);
        
        return response.ToActionResult();
    }
    
    [HttpGet("check/{advertId:int}")]
    public async Task<IActionResult> CheckDialogByAdvertId([FromRoute] int advertId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var query = new CheckDialogByAdvertIdQuery(userId, advertId);
        var response = await sender.Send(query, cancellationToken);

        return response.ToActionResult();
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateDialog([FromBody] CreateDialogRequest request,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var name = User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
        var command = new CreateDialogCommand(userId, name, request.AdvertId, request.Text);
        var response = await sender.Send(command, cancellationToken);
        
        return response.ToActionResult();
    }
    
}
using Chat.Core.CQRS.Commands.CreateMessage;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(int dialogId, CreateMessageResponse message)
    {
        await Clients.Group(dialogId.ToString()).SendAsync("ReceiveMessage", message);
    }
    
    public async Task JoinGroup(int dialogId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, dialogId.ToString());
    }
    
    public async Task LeaveGroup(int dialogId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, dialogId.ToString());
    }
}
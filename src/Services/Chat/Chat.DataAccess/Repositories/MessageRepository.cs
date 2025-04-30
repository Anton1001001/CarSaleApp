using Chat.Core.Abstractions;
using Chat.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Repositories;

public class MessageRepository(ChatDbContext context) : IMessageRepository
{
    
    public async Task<List<Message>> GetByDialogIdAsync(int dialogId, CancellationToken cancellationToken = default)
    {
        var messages = await context.Messages
            .Where(message => message.DialogId == dialogId)
            .ToListAsync(cancellationToken);
        
        return messages;
    }

    public async Task<Message> CreateAsync(Message message, CancellationToken cancellationToken = default)
    {
        var messageEntry =  await context.Messages.AddAsync(message, cancellationToken);
        
        return messageEntry.Entity;
    }
}
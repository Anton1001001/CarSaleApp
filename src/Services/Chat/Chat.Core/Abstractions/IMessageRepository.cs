using Chat.Core.Entities;

namespace Chat.Core.Abstractions;

public interface IMessageRepository
{
    Task<List<Message>> GetByDialogIdAsync(int dialogId, CancellationToken cancellationToken);
    Task<Message> CreateAsync(Message message, CancellationToken cancellationToken);
}
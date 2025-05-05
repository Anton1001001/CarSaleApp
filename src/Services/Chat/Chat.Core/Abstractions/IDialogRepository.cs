using Chat.Core.Entities;

namespace Chat.Core.Abstractions;

public interface IDialogRepository
{
    Task<Dialog?> GetByIdAsync(int dialogId, CancellationToken cancellationToken);
    Task<List<Dialog>> GetDialogsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Dialog?> GetDialogAsync(int advertId, Guid sellerId, Guid buyerId, CancellationToken cancellationToken);
    Task<Dialog> CreateAsync(Dialog dialog, CancellationToken cancellationToken);
}

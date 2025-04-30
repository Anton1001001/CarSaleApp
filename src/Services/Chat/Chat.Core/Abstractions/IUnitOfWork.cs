namespace Chat.Core.Abstractions;

public interface IUnitOfWork
{
    IMessageRepository MessageRepository { get; }
    IDialogRepository DialogRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
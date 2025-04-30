using Chat.Core.Abstractions;

namespace Chat.DataAccess.Repositories;

public class UnitOfWork(
    IMessageRepository messageRepository, 
    IDialogRepository dialogRepository, 
    ChatDbContext context)
    : IUnitOfWork
{
    public IMessageRepository MessageRepository { get; } = messageRepository;
    public IDialogRepository DialogRepository { get; } = dialogRepository;

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            
            return true;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            
            return false;
        }
    }
}
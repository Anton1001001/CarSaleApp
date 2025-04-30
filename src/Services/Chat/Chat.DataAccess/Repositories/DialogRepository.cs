using Chat.Core.Abstractions;
using Chat.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Repositories;

public class DialogRepository(ChatDbContext context) : IDialogRepository
{
    public async Task<Dialog?> GetByIdAsync(int dialogId, CancellationToken cancellationToken)
    {
        var dialog = await context.Dialogs
            .FirstOrDefaultAsync(dialog => dialog.Id == dialogId, cancellationToken);

        return dialog;
    }

    public async Task<List<Dialog>> GetDialogsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Dialogs
            .Where(dialog => dialog.SellerId == userId || dialog.BuyerId == userId)
            .OrderByDescending(dialog => dialog.LastMessageTime)
            .ToListAsync(cancellationToken);
    }

    public Task<Dialog?> GetDialogAsync(int advertId, Guid sellerId, Guid buyerId, CancellationToken cancellationToken)
    {
        var dialog = context.Dialogs
            .Where(dialog => 
                dialog.AdvertId == advertId && 
                dialog.SellerId == sellerId && 
                dialog.BuyerId == buyerId)
            .FirstOrDefaultAsync(cancellationToken);
        
        return dialog;
    }

    public async Task<Dialog> CreateAsync(Dialog dialog, CancellationToken cancellationToken)
    {
        var dialogEntry = await context.Dialogs.AddAsync(dialog, cancellationToken);
        
        return dialogEntry.Entity;
    }
}
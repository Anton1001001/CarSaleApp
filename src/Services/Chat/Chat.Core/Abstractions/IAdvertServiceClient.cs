using Chat.Core.CQRS.Queries.GetUserDialogs;

namespace Chat.Core.Abstractions;

public interface IAdvertServiceClient
{
    Task<List<AdvertPreviewResponse>> GetAdvertsPreviewsByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    Task<AdvertPreviewResponse?> GetAdvertPreviewByIdAsync(int id, CancellationToken cancellationToken);
}


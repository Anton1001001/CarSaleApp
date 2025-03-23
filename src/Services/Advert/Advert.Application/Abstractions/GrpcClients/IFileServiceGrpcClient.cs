using Advert.Application.Common.Advert.Models;

namespace Advert.Application.Abstractions.GrpcClients;

public interface IFileServiceGrpcClient
{
    Task<List<PhotoResponse>> GetFilesByIdsResponseAsync(List<int> ids, CancellationToken cancellationToken);
}
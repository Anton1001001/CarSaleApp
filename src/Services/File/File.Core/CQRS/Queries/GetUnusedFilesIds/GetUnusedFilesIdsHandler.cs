using File.Core.Abstractions;
using MediatR;

namespace File.Core.CQRS.Queries.GetUnusedFilesIds;

public class GetUnusedFilesIdsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUnusedFilesIdsQuery, GetUnusedFilesIdsResponse>
{
    public async Task<GetUnusedFilesIdsResponse> Handle(GetUnusedFilesIdsQuery request, CancellationToken cancellationToken)
    {
        var inactivePhotosIds = await unitOfWork.PhotoRepository.GetInactivePhotoIdsAsync(request.Ids, cancellationToken);
        return new GetUnusedFilesIdsResponse(inactivePhotosIds);
    }
}
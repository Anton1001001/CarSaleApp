using AutoMapper;
using File.Core.Abstractions;
using File.Core.Common.Models;
using File.Core.Errors;
using FluentResults;
using MediatR;

namespace File.Core.CQRS.Queries.GetFilesByIds;

public class GetFilesByIdsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetFilesByIdsQuery, Result<List<PhotoResponse>>>
{
    public async Task<Result<List<PhotoResponse>>> Handle(GetFilesByIdsQuery request,
        CancellationToken cancellationToken)
    {
        var photos = await unitOfWork.PhotoRepository.GetByIdsAsync(request.Ids, cancellationToken);
        var response = mapper.Map<List<PhotoResponse>>(photos);

        if (response.Count == 0)
        {
            return new FileNotFoundError(message: "No photos were found");
        }

        return response;
    }
}
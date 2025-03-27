using AutoMapper;
using File.Core.Abstractions;
using File.Core.Common.Models;
using File.Core.Errors;
using FluentResults;
using MediatR;

namespace File.Core.CQRS.Queries.GetFileById;

public class GetFileByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetFileByIdQuery, Result<PhotoResponse>>
{
    public async Task<Result<PhotoResponse>> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
    {
        var photo = await unitOfWork.PhotoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (photo is null)
        {
            return new FileNotFoundError(message: $"Photo with id: {request.Id} was not found");
        }
        
        var response = mapper.Map<PhotoResponse>(photo);
        return response;
    }
}
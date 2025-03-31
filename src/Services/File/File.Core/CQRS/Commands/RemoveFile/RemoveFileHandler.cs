using File.Core.Abstractions;
using File.Core.Errors.Base;
using FluentResults;
using MediatR;

namespace File.Core.CQRS.Commands.RemoveFile;

public class RemoveFileHandler(IStorageService storageService, IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveFileCommand, Result<RemoveFileResponse>>
{
    public async Task<Result<RemoveFileResponse>> Handle(RemoveFileCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.PhotoRepository.DeleteAsync(request.Ids, cancellationToken);
        await storageService.RemoveFilesAsync(request.Ids);

        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (!saveResult)
            return new InternalServerError("File.Remove", "Failed to save data");

        return new RemoveFileResponse();
    }
}
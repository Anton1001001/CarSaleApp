using FluentResults;
using MediatR;

namespace File.Core.CQRS.Commands.RemoveFile;

public record RemoveFileCommand(List<int> Ids) : IRequest<Result<RemoveFileResponse>>;
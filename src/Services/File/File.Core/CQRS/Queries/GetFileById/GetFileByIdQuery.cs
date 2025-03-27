using File.Core.Common.Models;
using FluentResults;
using MediatR;

namespace File.Core.CQRS.Queries.GetFileById;

public record GetFileByIdQuery(int Id) : IRequest<Result<PhotoResponse>>;
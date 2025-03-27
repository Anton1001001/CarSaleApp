using File.Core.Common.Models;
using FluentResults;
using MediatR;

namespace File.Core.CQRS.Queries.GetFilesByIds;

public record GetFilesByIdsQuery(List<int> Ids) : IRequest<Result<List<PhotoResponse>>>;
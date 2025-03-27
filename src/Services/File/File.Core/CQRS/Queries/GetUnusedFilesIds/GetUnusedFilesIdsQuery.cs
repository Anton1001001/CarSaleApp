using MediatR;

namespace File.Core.CQRS.Queries.GetUnusedFilesIds;

public record GetUnusedFilesIdsQuery(List<int> Ids) : IRequest<GetUnusedFilesIdsResponse>;

namespace Car.Application.Queries.Modification.GetModificationById;

public record GetModificationByIdQuery(int Id) : IRequest<GetModificationByIdResponse>;
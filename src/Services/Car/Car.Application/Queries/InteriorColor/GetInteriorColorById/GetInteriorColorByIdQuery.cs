namespace Car.Application.Queries.InteriorColor.GetInteriorColorById;

public record GetInteriorColorByIdQuery(int Id) : IRequest<GetInteriorColorByIdResponse>;
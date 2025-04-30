namespace Car.Application.Queries.BodyType.GetBodyTypeById;

public record GetBodyTypeByIdQuery(int Id) : IRequest<GetBodyTypeByIdResponse>;
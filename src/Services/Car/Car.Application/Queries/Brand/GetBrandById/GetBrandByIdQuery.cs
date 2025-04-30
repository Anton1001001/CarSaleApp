namespace Car.Application.Queries.Brand.GetBrandById;

public record GetBrandByIdQuery(int Id) : IRequest<GetBrandByIdResponse>;
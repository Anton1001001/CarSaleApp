namespace Car.Application.Queries.InteriorMaterial.GetInteriorMaterialById;

public record GetInteriorMaterialByIdQuery(int Id) : IRequest<GetInteriorMaterialByIdResponse>;
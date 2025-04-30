namespace Car.Application.Queries.InteriorMaterial.GetInteriorMaterialById;

public class GetInteriorMaterialByIdHandler(IRepository<CarInteriorMaterial> interiorMaterialRepository) : IRequestHandler<GetInteriorMaterialByIdQuery, GetInteriorMaterialByIdResponse>
{
    public async Task<GetInteriorMaterialByIdResponse> Handle(GetInteriorMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        var interiorMaterial = await interiorMaterialRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetInteriorMaterialByIdResponse(interiorMaterial.Id, interiorMaterial.Name);
    }
}
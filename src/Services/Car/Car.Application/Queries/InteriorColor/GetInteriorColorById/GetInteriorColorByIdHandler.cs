namespace Car.Application.Queries.InteriorColor.GetInteriorColorById;

public class GetInteriorColorByIdHandler(IRepository<CarInteriorColor> interiorColorRepository) : IRequestHandler<GetInteriorColorByIdQuery, GetInteriorColorByIdResponse>
{
    public async Task<GetInteriorColorByIdResponse> Handle(GetInteriorColorByIdQuery request, CancellationToken cancellationToken)
    {
        var interiorColor = await interiorColorRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetInteriorColorByIdResponse(interiorColor.Id, interiorColor.Name);
    }
}
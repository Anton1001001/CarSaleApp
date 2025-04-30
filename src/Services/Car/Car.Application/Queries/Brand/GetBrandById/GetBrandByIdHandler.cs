namespace Car.Application.Queries.Brand.GetBrandById;

public class GetBrandByIdHandler(ICarBrandRepository carBrandRepository) : IRequestHandler<GetBrandByIdQuery, GetBrandByIdResponse>
{
    public async Task<GetBrandByIdResponse> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await carBrandRepository.GetByIdAsync(request.Id, cancellationToken);
        var result = new GetBrandByIdResponse(request.Id, brand!.Name);
        
        return result;
    }
}
namespace Car.Application.Queries.BodyType.GetBodyTypeById;

public class GetBodyTypeByIdHandler(IRepository<CarBodyType> bodyTypeRepository) : IRequestHandler<GetBodyTypeByIdQuery, GetBodyTypeByIdResponse>
{
    public async Task<GetBodyTypeByIdResponse> Handle(GetBodyTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var bodyType = await bodyTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        
        return new GetBodyTypeByIdResponse(bodyType.Id, bodyType.Name);
    }
}
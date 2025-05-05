namespace Car.Application.Queries.EngineType.GetEngineTypeById;

public class GetEngineTypeByIdHandler(IRepository<CarEngineType> engineTypeRepository)
    : IRequestHandler<GetEngineTypeByIdQuery, GetEngineTypeByIdResponse>
{
    public async Task<GetEngineTypeByIdResponse> Handle(GetEngineTypeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var engineType = await engineTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetEngineTypeByIdResponse(engineType.Id, engineType.Name);
    }
}
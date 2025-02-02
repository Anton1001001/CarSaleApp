namespace Car.Application.Queries.GetBodyTypes;

public class GetBodyTypesHandler(IRepository<CarBodyType> bodyTypeRepository, IMapper mapper)
    : IRequestHandler<GetBodyTypesQuery, List<GetBodyTypesResponse>>
{
    public async Task<List<GetBodyTypesResponse>> Handle(GetBodyTypesQuery request, CancellationToken cancellationToken)
    {
        var bodyTypes = await bodyTypeRepository.GetAllAsync();
        var result = mapper.Map<List<GetBodyTypesResponse>>(bodyTypes);
        return result;
    }
}
namespace Car.Application.Queries.TransmissionType.GetTransmissionTypes;

public class GetTransmissionTypesHandler(IRepository<CarTransmissionType> transmissionTypeRepository, IMapper mapper)
    : IRequestHandler<GetTransmissionTypesQuery, List<GetTransmissionTypesResponse>>
{
    public async Task<List<GetTransmissionTypesResponse>> Handle(GetTransmissionTypesQuery request,
        CancellationToken cancellationToken)
    {
        var transmissionTypes = await transmissionTypeRepository.GetAllAsync();
        var result = mapper.Map<List<GetTransmissionTypesResponse>>(transmissionTypes);
        return result;
    }
}
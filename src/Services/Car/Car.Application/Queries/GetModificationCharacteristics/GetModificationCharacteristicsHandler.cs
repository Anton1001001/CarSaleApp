namespace Car.Application.Queries.GetModificationCharacteristics;

public class GetModificationCharacteristicsHandler(ICarModificationRepository modificationRepository, IMapper mapper)
    : IRequestHandler<GetModificationCharacteristicsQuery,
        List<GetModificationCharacteristicsResponse>>
{
    public async Task<List<GetModificationCharacteristicsResponse>> Handle(GetModificationCharacteristicsQuery request,
        CancellationToken cancellationToken)
    {
        var characteristicValues = await modificationRepository
            .GetCharacteristicsAsync(request.ModificationId, cancellationToken);

        var result = characteristicValues
            .GroupBy(cv => cv.CarCharacteristicNavigation.ParentId)
            .Select(group => new GetModificationCharacteristicsResponse
            (
                group.Key,
                group.First().CarCharacteristicNavigation.ParentNavigation?.Name,
                mapper.Map<List<CharacteristicValueResponse>>(group.ToList())
            ))
            .ToList();

        return result;
    }
}
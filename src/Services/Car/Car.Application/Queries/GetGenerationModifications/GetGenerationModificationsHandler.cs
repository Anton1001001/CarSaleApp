namespace Car.Application.Queries.GetGenerationModifications;

public class GetGenerationModificationsHandler(ICarGenerationRepository generationRepository, IMapper mapper)
    : IRequestHandler<GetGenerationModificationsQuery,
        List<GetGenerationModificationsResponse>>
{
    public async Task<List<GetGenerationModificationsResponse>> Handle(GetGenerationModificationsQuery request,
        CancellationToken cancellationToken)
    {
        var modifications = await generationRepository
            .GetModificationsAsync(request.GenerationId, cancellationToken);
        var result = mapper.Map<List<GetGenerationModificationsResponse>>(modifications);
        return result;
    }
}
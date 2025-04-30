namespace Car.Application.Queries.Generation.GetGenerationById;

public class GetGenerationByIdHandler(ICarGenerationRepository generationRepository) : IRequestHandler<GetGenerationByIdQuery, GetGenerationByIdResponse>
{
    public async Task<GetGenerationByIdResponse> Handle(GetGenerationByIdQuery request, CancellationToken cancellationToken)
    {
        var generation = await generationRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetGenerationByIdResponse(generation.Id, generation.Name, generation.YearBegin, generation.YearEnd);
    }
}
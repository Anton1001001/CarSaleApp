namespace Car.Application.Queries.Year.GetModelYears;

public class GetModelYearsHandler(ICarModelRepository modelRepository) : IRequestHandler<GetModelYearsQuery, List<ModelYearResponse>>
{
    public async Task<List<ModelYearResponse>> Handle(GetModelYearsQuery request, CancellationToken cancellationToken)
    {
        var generations = await modelRepository.GetGenerationsAsync(request.ModelId, cancellationToken);
        var response = generations
            .SelectMany(generation => Enumerable.Range(
                generation.YearBegin,
                (generation.YearEnd ?? DateTime.Now.Year) - generation.YearBegin + 1
            ))
            .Distinct()
            .OrderDescending()
            .Select(year => new ModelYearResponse { Year = year })
            .ToList();
        
        return response;
    }
}
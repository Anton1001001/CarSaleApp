namespace Car.Application.Queries.Year.GetModelYears;

public class GetModelYearsQuery : IRequest<List<ModelYearResponse>>
{
    public int ModelId { get; set; }
}
namespace Car.Application.Queries.GetColors;

public class GetColorsHandler(IRepository<CarColor> colorRepository, IMapper mapper) : IRequestHandler<GetColorsQuery, List<GetColorsResponse>>
{
    public async Task<List<GetColorsResponse>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
    {
        var colors = await colorRepository.GetAllAsync();
        var result = mapper.Map<List<GetColorsResponse>>(colors);
        return result;
    }
}
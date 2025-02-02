namespace Car.Application.Queries.GetInteriorColors;

public class GetInteriorColorsHandler(IRepository<CarInteriorColor> interiorColorRepository, IMapper mapper)
    : IRequestHandler<GetInteriorColorsQuery, List<GetInteriorColorsResponse>>
{
    public async Task<List<GetInteriorColorsResponse>> Handle(GetInteriorColorsQuery request,
        CancellationToken cancellationToken)
    {
        var interiorColors = await interiorColorRepository.GetAllAsync();
        var result = mapper.Map<List<GetInteriorColorsResponse>>(interiorColors);
        return result;
    }
}
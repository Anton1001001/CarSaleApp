namespace Car.Application.Queries.GetBrands;

public class GetBrandsHandler(ICarBrandRepository carBrandRepository, IMapper mapper)
    : IRequestHandler<GetBrandsQuery, List<GetBrandsResponse>>
{
    public async Task<List<GetBrandsResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await carBrandRepository.GetAllAsync();
        var result = mapper.Map<List<GetBrandsResponse>>(brands);
        return result;
    }
}
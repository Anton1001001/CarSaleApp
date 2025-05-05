namespace Car.Application.Queries.Color.GetColorById;

public class GetColorByIdHandler(IRepository<CarColor> colorRepository) : IRequestHandler<GetColorByIdQuery, GetColorByIdResponse>
{
    public async Task<GetColorByIdResponse> Handle(GetColorByIdQuery request, CancellationToken cancellationToken)
    {
        var color = await colorRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetColorByIdResponse(color.Id, color.Name);
    }
}
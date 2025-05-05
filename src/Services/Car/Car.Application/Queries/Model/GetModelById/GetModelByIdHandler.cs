namespace Car.Application.Queries.Model.GetModelById;

public class GetModelByIdHandler(ICarModelRepository modelRepository) : IRequestHandler<GetModelByIdQuery, GetModelByIdResponse>
{
    public async Task<GetModelByIdResponse> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await modelRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetModelByIdResponse(model!.Id, model.Name);
    }
}
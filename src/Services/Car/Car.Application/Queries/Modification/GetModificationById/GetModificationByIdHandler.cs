namespace Car.Application.Queries.Modification.GetModificationById;

public class GetModificationByIdHandler(IRepository<CarModification> modificationRepository) : IRequestHandler<GetModificationByIdQuery, GetModificationByIdResponse>
{
    public async Task<GetModificationByIdResponse> Handle(GetModificationByIdQuery request, CancellationToken cancellationToken)
    {
        var modification = await modificationRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetModificationByIdResponse(modification.Id, modification.Name);
    }
}
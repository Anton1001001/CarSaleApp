namespace Car.Application.Queries.TransmissionType.GetTransmissionTypeById;

public class GetTransmissionTypeByIdHandler(IRepository<CarTransmissionType> transmissionTypeRepository) : IRequestHandler<GetTransmissionTypeByIdQuery, GetTransmissionTypeByIdResponse>
{
    public async Task<GetTransmissionTypeByIdResponse> Handle(GetTransmissionTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var transmissionType = await transmissionTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetTransmissionTypeByIdResponse(transmissionType.Id, transmissionType.Name);
    }
}
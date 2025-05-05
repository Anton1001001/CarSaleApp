namespace Car.Application.Queries.TransmissionType.GetTransmissionTypeById;

public record GetTransmissionTypeByIdQuery(int Id) : IRequest<GetTransmissionTypeByIdResponse>;
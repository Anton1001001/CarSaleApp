namespace Car.Application.Queries.GetModificationCharacteristics;

public record GetModificationCharacteristicsQuery(int ModificationId) : IRequest<List<GetModificationCharacteristicsResponse>>;
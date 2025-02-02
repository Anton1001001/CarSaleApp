namespace Car.Application.Queries.GetModificationCharacteristics;

public record GetModificationCharacteristicsResponse(
    int? ParentId,
    string? ParentName,
    List<CharacteristicValueResponse> Characteristics);
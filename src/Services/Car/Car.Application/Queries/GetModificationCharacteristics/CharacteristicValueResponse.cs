namespace Car.Application.Queries.GetModificationCharacteristics;

public record CharacteristicValueResponse(
    CharacteristicResponse Characteristic,
    int Id, 
    string? Value,
    string? Unit);
namespace Car.Application.Queries.GetModelGenerations;

public record GetModelGenerationsResponse(
    int Id,
    string Name,
    string? YearBegin,
    string? YearEnd,
    List<BodyTypeResponse> CarBodyTypes);
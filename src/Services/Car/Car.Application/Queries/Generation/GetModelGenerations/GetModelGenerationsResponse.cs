namespace Car.Application.Queries.Generation.GetModelGenerations;

public record GetModelGenerationsResponse(
    int Id,
    string Name,
    int YearBegin,
    int? YearEnd,
    List<BodyTypeResponse> CarBodyTypes);
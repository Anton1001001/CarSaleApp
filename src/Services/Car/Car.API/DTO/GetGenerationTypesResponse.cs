namespace Car.API.DTO;
public record GetGenerationTypesResponse(
    int Id,
    string Name,
    int? YearBegin,
    int? YearEnd,
    List<GetBodyTypesResponse> CarBodyTypes);
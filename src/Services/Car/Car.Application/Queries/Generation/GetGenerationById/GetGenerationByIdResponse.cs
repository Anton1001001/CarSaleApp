namespace Car.Application.Queries.Generation.GetGenerationById;

public record GetGenerationByIdResponse(int Id, string Name, int YearBegin, int? YearEnd);
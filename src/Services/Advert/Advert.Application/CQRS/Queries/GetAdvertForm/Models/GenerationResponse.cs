namespace Advert.Application.CQRS.Queries.GetAdvertForm.Models;

public record GenerationResponse(
    int Id, 
    string Name, 
    int YearBegin, 
    int? YearEnd, 
    List<BodyTypeResponse>? BodyTypes);

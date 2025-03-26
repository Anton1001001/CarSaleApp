namespace Advert.Application.Common.Advert.Models;

public record PrivateStatusResponse(
    string Name,
    string? Label,
    bool Published,
    string? PhotoLabel
);


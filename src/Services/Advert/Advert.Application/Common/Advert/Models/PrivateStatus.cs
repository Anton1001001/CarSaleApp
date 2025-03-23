namespace Advert.Application.Common.Advert.Models;

public record PrivateStatus(
    string Name,
    string? Label,
    bool Published,
    string? PhotoLabel
);


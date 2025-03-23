namespace Advert.Application.Common.Advert.Models;

// public class PrivateStatus
// {
//     public string Name { get; set; }
//     public string? Label { get; set; }
//     public bool Published { get; set; }
//     public string? PhotoLabel { get; set; }
// }

public record PrivateStatus(
    string Name,
    string? Label,
    bool Published,
    string? PhotoLabel
);


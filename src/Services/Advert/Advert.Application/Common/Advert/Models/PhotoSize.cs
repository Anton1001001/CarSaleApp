namespace Advert.Application.Common.Advert.Models;

// public class PhotoSize
// {
//     public int Width { get; set; }
//     public int Height { get; set; }
//     public string Url { get; set; } = string.Empty;
// }

public record PhotoSize(
    int Width,
    int Height,
    string Url
);


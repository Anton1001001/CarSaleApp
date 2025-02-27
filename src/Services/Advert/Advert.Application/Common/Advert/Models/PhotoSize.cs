namespace Advert.Application.CQRS.Commands.CreateAdvert;

public class PhotoSize
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string Url { get; set; } = string.Empty;
}

namespace Advert.Application.CQRS.Commands.CreateAdvert;

public class PhotoResponse
{
    public long Id { get; set; }
    public bool Main { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public PhotoSize Big { get; set; } = new();
    public PhotoSize Medium { get; set; } = new();
    public PhotoSize Small { get; set; } = new();
    public PhotoSize ExtraSmall { get; set; } = new(); 
}


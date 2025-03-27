namespace File.Core.Models;

public class Photo
{
    public int Id { get; set; }
    
    public string MimeType { get; set; }
    public PhotoSize Big { get; set; }
    public PhotoSize Medium { get; set; }
    public PhotoSize Small { get; set; }
    public PhotoSize ExtraSmall { get; set; }
}



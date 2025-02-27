namespace Advert.Application.CQRS.Commands.CreateAdvert.Parameters;

public class PhotoRequest
{
    public List<int> Files { get; set; }
    public long Main { get; set; }
}
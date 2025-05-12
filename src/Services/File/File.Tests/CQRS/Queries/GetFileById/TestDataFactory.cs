using File.Core.Common.Models;
using File.Core.CQRS.Queries.GetFileById;
using File.Core.Models;

namespace File.Tests.CQRS.Queries.GetFileById;

public static class TestDataFactory
{
    public const int CorrectPhotoId = 1;
    public const int IncorrectPhotoId = 2;
    public static GetFileByIdQuery CreateGetFileByIdQuery(int id) => new(id);

    public static Photo CreatePhoto() => new()
    {
        Id = 1,
        MimeType = "image/png",
        Big = new PhotoSize { Width = 1000, Height = 800, Url = "url-big" },
        Medium = new PhotoSize { Width = 800, Height = 600, Url = "url-medium" },
        Small = new PhotoSize { Width = 400, Height = 300, Url = "url-small" },
        ExtraSmall = new PhotoSize { Width = 200, Height = 150, Url = "url-xs" }
    };

    public static PhotoResponse CreatePhotoResponse() => new(
        Id: 1,
        Big: new PhotoSizeResponse(1000, 800, "url-big"),
        Medium: new PhotoSizeResponse(800, 600, "url-medium"),
        Small: new PhotoSizeResponse(400, 300, "url-small"),
        ExtraSmall: new PhotoSizeResponse(200, 150, "url-xs")
    );
}
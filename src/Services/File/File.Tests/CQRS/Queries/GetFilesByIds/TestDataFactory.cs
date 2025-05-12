using File.Core.Common.Models;
using File.Core.CQRS.Queries.GetFilesByIds;
using File.Core.Models;

namespace File.Tests.CQRS.Queries.GetFilesByIds;

public static class TestDataFactory
{
    public static readonly List<int> CorrectIds = [1, 2, 3];
    public static readonly List<int> IncorrectIds = [4, 5, 6];
    
    public static GetFilesByIdsQuery CreateGetFilesByIdsQuery(List<int> ids)
    {
        return new GetFilesByIdsQuery(ids);
    }

    public static List<Photo> CreatePhotos()
    {
        return
        [
            new Photo { Id = 1, MimeType = "image/jpeg" },
            new Photo { Id = 2, MimeType = "image/png" },
            new Photo { Id = 3, MimeType = "image/gif" }
        ];
    }

    public static List<PhotoResponse> CreatePhotoResponses()
    {
        return
        [
            new PhotoResponse(1, new PhotoSizeResponse(100, 100, "url1"), new PhotoSizeResponse(50, 50, "url2"),
                new PhotoSizeResponse(25, 25, "url3"), new PhotoSizeResponse(10, 10, "url4")),
            new PhotoResponse(2, new PhotoSizeResponse(100, 100, "url1"), new PhotoSizeResponse(50, 50, "url2"),
                new PhotoSizeResponse(25, 25, "url3"), new PhotoSizeResponse(10, 10, "url4")),
            new PhotoResponse(3, new PhotoSizeResponse(100, 100, "url1"), new PhotoSizeResponse(50, 50, "url2"),
                new PhotoSizeResponse(25, 25, "url3"), new PhotoSizeResponse(10, 10, "url4"))
        ];
    }
}

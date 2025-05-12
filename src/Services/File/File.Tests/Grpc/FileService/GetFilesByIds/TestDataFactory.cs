using File.Core.Common.Models;
using File.GrpcService;

namespace File.Tests.Grpc.FileService.GetFilesByIds;

public static class TestDataFactory
{
    public static GetFilesByIdsRequest CreateGetFilesByIdsRequest() =>
        new() { Ids = { 1, 2, 3 } };

    public static List<PhotoResponse> CreatePhotoResponses() => new()
    {
        new PhotoResponse(1, CreateSize(), CreateSize(), CreateSize(), CreateSize()),
        new PhotoResponse(2, CreateSize(), CreateSize(), CreateSize(), CreateSize()),
        new PhotoResponse(3, CreateSize(), CreateSize(), CreateSize(), CreateSize())
    };

    public static List<FileResponse> CreateFileResponses() => new()
    {
        new FileResponse { Id = 1, MimeType = "image/jpeg", Big = CreateGrpcSize(), Medium = CreateGrpcSize(), Small = CreateGrpcSize(), ExtraSmall = CreateGrpcSize() },
        new FileResponse { Id = 2, MimeType = "image/png", Big = CreateGrpcSize(), Medium = CreateGrpcSize(), Small = CreateGrpcSize(), ExtraSmall = CreateGrpcSize() },
        new FileResponse { Id = 3, MimeType = "image/webp", Big = CreateGrpcSize(), Medium = CreateGrpcSize(), Small = CreateGrpcSize(), ExtraSmall = CreateGrpcSize() }
    };

    public static PhotoSizeResponse CreateSize() => new(100, 100, "http://example.com/img.jpg");

    public static PhotoSize CreateGrpcSize() => new PhotoSize { Url = "http://example.com/img.jpg", Width = 100, Height = 100 };

}

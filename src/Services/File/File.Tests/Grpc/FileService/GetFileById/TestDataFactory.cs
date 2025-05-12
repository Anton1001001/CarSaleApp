using File.Core.Common.Models;
using File.GrpcService;

namespace File.Tests.Grpc.FileService.GetFileById;

public static class TestDataFactory
{
    public static int FileId => 123;

    public const string Error = "File not found";

    public static PhotoResponse CreateDomainPhoto()
    {
        return new PhotoResponse(
            FileId,
            new PhotoSizeResponse(800, 600, "https://cdn/big.jpg"),
            new PhotoSizeResponse(640, 480, "https://cdn/medium.jpg"),
            new PhotoSizeResponse(320, 240, "https://cdn/small.jpg"),
            new PhotoSizeResponse(160, 120, "https://cdn/xsmall.jpg")
        );
    }

    public static FileResponse CreateGrpcFile()
    {
        return new FileResponse
        {
            Id = FileId,
            MimeType = "image/jpeg",
            Big = new PhotoSize { Url = "https://cdn/big.jpg", Width = 800, Height = 600 },
            Medium = new PhotoSize { Url = "https://cdn/medium.jpg", Width = 640, Height = 480 },
            Small = new PhotoSize { Url = "https://cdn/small.jpg", Width = 320, Height = 240 },
            ExtraSmall = new PhotoSize { Url = "https://cdn/xsmall.jpg", Width = 160, Height = 120 }
        };
    }
}


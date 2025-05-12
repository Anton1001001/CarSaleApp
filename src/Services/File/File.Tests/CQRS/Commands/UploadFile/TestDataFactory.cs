using File.Core.Options;
using Microsoft.AspNetCore.Http;
using Moq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace File.Tests.CQRS.Commands.UploadFile;

public static class TestDataFactory
{
    public static ImageSizeOptions CreateImageSizeOptions() => new()
    {
        MaxLength = 1000,
        MediumLength = 700,
        SmallLength = 400,
        ExtraSmallLength = 200
    };

    public static IFormFile CreateMockFormFile(string contentType)
    {
        var fileMock = new Mock<IFormFile>();
        var ms = new MemoryStream();
        using var image = new Image<Rgba32>(100, 100);
        image.SaveAsPng(ms);
        ms.Position = 0;

        fileMock.Setup(f => f.ContentType).Returns(contentType);
        fileMock.Setup(f => f.FileName).Returns("test.png");
        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);

        return fileMock.Object;
    }
}
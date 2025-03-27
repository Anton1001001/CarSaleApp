using File.Core.Options;
using SixLabors.ImageSharp;

namespace File.Core.Helpers;

public static class StreamConverter
{
    public static MemoryStream ConvertToStream(Image image, string mimeType)
    {
        var memoryStream = new MemoryStream();
        switch (mimeType)
        {
            case ImageFormats.Jpg:
                image.SaveAsJpeg(memoryStream);
                break;
            case ImageFormats.Png:
                image.SaveAsPng(memoryStream);
                break;
        }
        memoryStream.Position = 0;
        return memoryStream;
    }
}
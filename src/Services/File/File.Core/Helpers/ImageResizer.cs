using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace File.Core.Helpers;

public static class ImageResizer
{
    public static (int width, int height) CalculateSize(int originalWidth, int originalHeight, int maxSize)
    {
        if (originalWidth <= maxSize && originalHeight <= maxSize)
            return (originalWidth, originalHeight);

        double aspectRatio = originalWidth / (double)originalHeight;

        if (originalWidth >= originalHeight)
        {
            int width = maxSize;
            int height = (int)(maxSize / aspectRatio);

            return (width, height);
        }
        else
        {
            int height = maxSize;
            int width = (int)(maxSize * aspectRatio);

            return (width, height);
        }
    }

    public static Image ResizeImage(Image image, int width, int height)
    {
        var resizedImage = image.Clone(ctx => ctx.Resize(width, height));

        return resizedImage;
    }
}
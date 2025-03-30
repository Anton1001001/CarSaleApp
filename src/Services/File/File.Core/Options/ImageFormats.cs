namespace File.Core.Options;

public static class ImageFormats
{
    public const string Jpg = "image/jpeg";
    public const string Png = "image/png";
    private static readonly HashSet<string> AllowedTypes = [Jpg, Png];
    public static bool IsSupported(string mimeType) => AllowedTypes.Contains(mimeType);
}
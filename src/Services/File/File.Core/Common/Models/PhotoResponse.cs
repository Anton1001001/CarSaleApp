namespace File.Core.Common.Models;

public record PhotoResponse(
    int Id,
    PhotoSizeResponse Big, 
    PhotoSizeResponse Medium, 
    PhotoSizeResponse Small, 
    PhotoSizeResponse ExtraSmall);
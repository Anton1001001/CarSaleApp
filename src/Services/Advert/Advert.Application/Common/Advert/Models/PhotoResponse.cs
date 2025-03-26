namespace Advert.Application.Common.Advert.Models;

public record PhotoResponse(
    long Id,
    bool Main,
    string MimeType,
    PhotoSize Big,
    PhotoSize Medium,
    PhotoSize Small,
    PhotoSize ExtraSmall
);


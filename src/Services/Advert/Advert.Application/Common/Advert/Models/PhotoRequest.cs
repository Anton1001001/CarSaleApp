namespace Advert.Application.Common.Advert.Models;

public record PhotoRequest(
    List<int> Files,
    long? Main);

namespace Chat.Core.CQRS.Queries.GetUserDialogs;

public record PriceResponse(
    int Usd,
    int Byn,
    int Rub,
    int Eur
);
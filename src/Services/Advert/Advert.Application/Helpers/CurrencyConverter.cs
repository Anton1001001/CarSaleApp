using Advert.Application.Abstractions;
using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Commands.CreateAdvert;
using Advert.Domain.Enums;

namespace Advert.Application.Helpers;

public class CurrencyConverter(ICurrencyRateService currencyRateService) : ICurrencyConverter
{
    public async Task<PriceResponse?> ConvertPriceToAllCurrenciesOrDefaultAsync(int amount, Currency currency, CancellationToken cancellationToken = default)
    {
        var rates = await currencyRateService.GetCurrencyRatesAsync(cancellationToken);
        var amountInByn = currency switch
        {
            Currency.Usd => amount * rates.Usd,
            Currency.Eur => amount * rates.Eur,
            Currency.Rub => amount * rates.Rub,
            Currency.Byn => amount,
            _ => default(decimal?)
        };

        if (amountInByn is null)
        {
            return null;
        }

        return new PriceResponse(
            Usd: Convert.ToInt32(amountInByn / rates.Usd),
            Eur: Convert.ToInt32(amountInByn / rates.Eur),
            Rub: Convert.ToInt32(amountInByn / rates.Rub),
            Byn: Convert.ToInt32(amountInByn)
        );
    }
}
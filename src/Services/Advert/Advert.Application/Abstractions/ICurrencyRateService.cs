using Advert.Application.Helpers;

namespace Advert.Application.Abstractions;

public interface ICurrencyRateService
{
    Task<CurrencyRates> GetCurrencyRatesAsync(CancellationToken cancellationToken);
}

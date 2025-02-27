using Advert.Application.Helpers;

namespace Advert.Application.Interfaces;

public interface ICurrencyRateService
{
    Task<CurrencyRates> GetCurrencyRatesAsync();
}

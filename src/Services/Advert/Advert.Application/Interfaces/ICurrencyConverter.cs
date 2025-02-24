using Advert.Application.CQRS.Commands.CreateAdvert;
using Advert.Domain.Enums;

namespace Advert.Application.Interfaces;

public interface ICurrencyConverter
{
    Task<PriceResponse> ConvertPriceToAllCurrenciesAsync(int amount, Currency currency);
}
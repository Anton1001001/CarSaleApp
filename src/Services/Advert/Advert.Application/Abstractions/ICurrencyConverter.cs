using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Commands.CreateAdvert;
using Advert.Domain.Enums;

namespace Advert.Application.Abstractions;

public interface ICurrencyConverter
{
    Task<PriceResponse> ConvertPriceToAllCurrenciesAsync(int amount, Currency currency, CancellationToken cancellationToken);
}
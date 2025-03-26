using Advert.Application.Abstractions;
using Advert.Application.Helpers;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
namespace Advert.Infrastructure.Services;

public class CurrencyRateService(IOptions<NbrbApiConfig> options, IDistributedCache cache) : ICurrencyRateService
{
    private static readonly HttpClient HttpClient = new();
    private static readonly TimeSpan CacheDuration = TimeSpan.FromDays(1); 

    public async Task<CurrencyRates> GetCurrencyRatesAsync(CancellationToken cancellationToken = default)
    {
        var nbrbOptions = options.Value;
        var cacheKey = "CurrencyRates";
        var cachedData = await cache.GetStringAsync(cacheKey, token: cancellationToken);
        
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonConvert.DeserializeObject<CurrencyRates>(cachedData)!;
        }
        
        var response = await HttpClient.GetStringAsync(nbrbOptions.NbrbApiUrl, cancellationToken);
        var rateList = JsonConvert.DeserializeObject<List<NbrbRate>>(response);

        if (rateList is null || rateList.Count == 0)
        {
            throw new InvalidOperationException("Couldn't get a list of currency rates.");
        }
        
        var usdRate = rateList.Find(r => r.CurAbbreviation == nbrbOptions.Usd);
        var eurRate = rateList.Find(r => r.CurAbbreviation == nbrbOptions.Eur);
        var rubRate = rateList.Find(r => r.CurAbbreviation == nbrbOptions.Rub);

        var rates = new CurrencyRates
        {
            Byn = 1,
            Usd = usdRate is not null ? usdRate.CurOfficialRate / usdRate.CurScale : 0,
            Eur = eurRate is not null ? eurRate.CurOfficialRate / eurRate.CurScale : 0,
            Rub = rubRate is not null ? rubRate.CurOfficialRate / rubRate.CurScale : 0,
        };

        if (rates.Usd == 0 || rates.Eur == 0 || rates.Rub == 0)
        {
            throw new InvalidOperationException("Incorrect exchange rates");
        }
        
        await cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(rates), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheDuration
        }, token: cancellationToken);

        return rates;
    }

    public class NbrbRate
    {
        [JsonProperty("Cur_Abbreviation")] 
        public string CurAbbreviation { get; set; } = null!;

        [JsonProperty("Cur_OfficialRate")] 
        public decimal CurOfficialRate { get; set; }

        [JsonProperty("Cur_Scale")] 
        public decimal CurScale { get; set; }
    }
}
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

        var rates = new CurrencyRates
        {
            Byn = 1,
            Usd = rateList.Find(r => r.CurAbbreviation == nbrbOptions.Usd)?.CurOfficialRate /
                rateList.Find(r => r.CurAbbreviation == nbrbOptions.Usd)?.CurScale ?? 0,
            Eur = rateList.Find(r => r.CurAbbreviation == nbrbOptions.Eur)?.CurOfficialRate /
                rateList.Find(r => r.CurAbbreviation == nbrbOptions.Eur)?.CurScale ?? 0,
            Rub = rateList.Find(r => r.CurAbbreviation == nbrbOptions.Rub)?.CurOfficialRate /
                rateList.Find(r => r.CurAbbreviation == nbrbOptions.Rub)?.CurScale ?? 0
        };
        
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
using Advert.Application.Helpers;
using Advert.Application.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
namespace Advert.Infrastructure.Services;

public class CurrencyRateService(IOptions<NbrbApiConfig> options) : ICurrencyRateService
{
    private static readonly HttpClient HttpClient = new();

    public async Task<CurrencyRates> GetCurrencyRatesAsync()
    {
        var nbrbOptions = options.Value;
        var response = await HttpClient.GetStringAsync(nbrbOptions.NbrbApiUrl);
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

        return rates;
    }

    public class NbrbRate
    {
        [JsonProperty("Cur_Abbreviation")] 
        public string CurAbbreviation { get; set; }

        [JsonProperty("Cur_OfficialRate")] 
        public decimal CurOfficialRate { get; set; }

        [JsonProperty("Cur_Scale")] 
        public decimal CurScale { get; set; }
    }
}
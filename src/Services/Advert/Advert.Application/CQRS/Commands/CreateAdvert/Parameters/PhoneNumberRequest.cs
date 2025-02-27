using Newtonsoft.Json;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Parameters;

public class PhoneNumberRequest
{
    [JsonProperty("codeId")]
    public int PhoneCodeId { get; set; }
    public string Number { get; set; }
}
using Newtonsoft.Json.Linq;

namespace Advert.Application.CQRS.Commands.CreateAdvert;

public class CreateAdvertRequest
{
    public JObject Params { get; set; }
}
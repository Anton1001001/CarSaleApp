using Newtonsoft.Json.Linq;

namespace Advert.Application.CQRS.Commands.CreateAdvert;

public record CreateAdvertRequest(JObject Params);
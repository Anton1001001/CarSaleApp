using Newtonsoft.Json.Linq;

namespace Advert.Application.CQRS.Queries.GetAdvertForm;

public record GetAdvertFormRequest(JObject Params);
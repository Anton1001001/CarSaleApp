using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Parameters;

public static class ParametersFactory
{
    private static readonly Dictionary<string, Type> ParamsTypes;

    static ParametersFactory()
    {
        ParamsTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && typeof(ParametersBase).IsAssignableFrom(t))
            .ToDictionary(
                t => t.Name.Replace("Parameters", "").ToLower(),
                t => t
            );
    }

    public static CreateAdvertCommand CreateCommand(string type, string jsonData)
    {
        var paramsType = ParamsTypes[type.ToLower()];
        var paramsJson = JObject.Parse(jsonData).ToString();
        var paramsObject = JsonConvert.DeserializeObject(paramsJson, paramsType) as ParametersBase;
        return new CreateAdvertCommand { Parameters = paramsObject! };
    }
}

//
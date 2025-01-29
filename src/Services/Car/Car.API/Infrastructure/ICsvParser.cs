namespace Car.API.Infrastructure;

public interface ICsvParser
{
    IEnumerable<T> Parse<T>(string filePath, Func<string[], T> mapFunction);
}

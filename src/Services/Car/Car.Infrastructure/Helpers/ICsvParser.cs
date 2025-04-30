namespace Car.Infrastructure.Helpers;
public interface ICsvParser
{
    IEnumerable<T> Parse<T>(string filePath, Func<string[], T> mapFunction);
}

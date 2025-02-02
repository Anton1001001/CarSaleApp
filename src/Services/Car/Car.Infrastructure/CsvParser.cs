namespace Car.Infrastructure;

public class CsvParser : ICsvParser
{
    private const string Pattern = @"'((?:[^']|'')*)'|NULL";

    public IEnumerable<T> Parse<T>(string filePath, Func<string[], T> mapFunction)
    {
        var results = new List<T>();

        using var reader = new StreamReader(filePath);
        if (reader.ReadLine() is null)
            throw new Exception("File is empty");

        var values = new List<string?>();

        while (reader.ReadLine() is { } line)
        {
            var matches = Regex.Matches(line, Pattern);
            foreach (Match match in matches)
            {
                values.Add(match.Value == "NULL" ? null : match.Groups[1].Value);
            }

            var entity = mapFunction(values.ToArray()!);
            values.Clear();
            results.Add(entity);
        }

        return results;
    }
}
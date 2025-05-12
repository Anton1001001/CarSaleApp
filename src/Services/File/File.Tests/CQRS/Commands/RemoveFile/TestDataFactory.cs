using File.Core.CQRS.Commands.RemoveFile;

namespace File.Tests.CQRS.Commands.RemoveFile;

public static class TestDataFactory
{
    public static List<int> DefaultFileIds => new() { 1, 2, 3 };

    public static List<int> AlternativeFileIds => new() { 10, 20 };

    public static RemoveFileCommand CreateRemoveFileCommand()
        => new(DefaultFileIds);

    public static RemoveFileCommand CreateRemoveFileCommandWithDifferentIds()
        => new(AlternativeFileIds);
}

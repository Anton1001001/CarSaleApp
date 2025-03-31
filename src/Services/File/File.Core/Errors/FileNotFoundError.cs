using File.Core.Errors.Base;

namespace File.Core.Errors;

public class FileNotFoundError(string code = "File.NotFound", string message = "File not found")
    : NotFoundError(code, message);
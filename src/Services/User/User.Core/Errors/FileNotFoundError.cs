using User.Core.Errors.Base;

namespace User.Core.Errors;

public class FileNotFoundError(string code = "File.NotFound", string message = "File not found")
    : NotFoundError(code, message);
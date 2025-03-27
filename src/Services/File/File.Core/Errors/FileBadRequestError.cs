using File.Core.Errors.Base;

namespace File.Core.Errors;

public class FileBadRequestError(string code = "File.BadRequest", string message = "Invalid request") :
    BadRequestError(code, message);
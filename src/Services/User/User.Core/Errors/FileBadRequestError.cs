using User.Core.Errors.Base;

namespace User.Core.Errors;

public class FileBadRequestError(string code = "File.BadRequest", string message = "Invalid request") :
    BadRequestError(code, message);
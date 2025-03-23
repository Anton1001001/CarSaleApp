using Advert.Application.Errors.Base;

namespace Advert.Application.Errors;

public class AdvertBadRequestError(string code = "Advert.BadRequest", string message = "Invalid request")
    : BadRequestError(code, message);
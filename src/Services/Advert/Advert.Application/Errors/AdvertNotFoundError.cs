using Advert.Application.Errors.Base;

namespace Advert.Application.Errors;

public class AdvertNotFoundError(string code = "Advert.NotFound", string message = "Advert not found")
    : NotFoundError(code, message);
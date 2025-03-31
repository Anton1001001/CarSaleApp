using Advert.Application.Errors.Base;

namespace Advert.Application.Errors;

public class CategoryNotFoundError(string code = "Category.NotFound", string message = "Category not found")
    : NotFoundError(code, message);
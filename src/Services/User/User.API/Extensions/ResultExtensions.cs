using FluentResults;
using Microsoft.AspNetCore.Mvc;
using User.Core.Errors.Base;

namespace User.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<TValue>(this Result<TValue> result)
    {
        return result.IsSuccess 
            ? new OkObjectResult(result.Value) 
            : result.ToProblemDetails();
    }

    public static IActionResult ToActionResult(this Result result)
    {
        return result.IsSuccess 
            ? new NoContentResult() 
            : result.ToProblemDetails();
    }

    private static IActionResult ToProblemDetails(this ResultBase result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot create a problem because the result is successful");
        }

        var error = result.Errors.FirstOrDefault()!;
        var extensions = new Dictionary<string, object?> { { "message", error.Message } };

        if (error is BaseError baseError)
        {
            extensions.Add("code", baseError.Code);

            if (baseError.Details is not null)
            {
                extensions.Add("details", baseError.Details);
            }
        }

        return new ObjectResult(new ProblemDetails
        {
            Status = GetStatusCode(error),
            Title = GetTitle(error),
            Extensions = extensions
        })
        {
            StatusCode = GetStatusCode(error)
        };
    }


    private static int GetStatusCode(IError error) => error switch
    {
        BadRequestError => StatusCodes.Status400BadRequest,
        UnauthorizedError => StatusCodes.Status401Unauthorized,
        NotFoundError => StatusCodes.Status404NotFound,
        ConflictError => StatusCodes.Status409Conflict,
        InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetTitle(IError error) => error switch
    {
        BadRequestError => "Bad request",
        UnauthorizedError => "Unauthorized",
        NotFoundError => "Not Found",
        ConflictError => "Conflict",
        InternalServerError => "Internal Server Error",
        _ => "Internal server error"
    };
}
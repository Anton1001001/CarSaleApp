using System.Net;
using Newtonsoft.Json;

namespace User.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = context.Response;
        var errorResponse = new ErrorResponse();

        switch (exception)
        {
            case ArgumentOutOfRangeException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Invalid range provided.";
                break;

            case InvalidOperationException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                errorResponse.Message = "Invalid operation.";
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "An unexpected error occurred.";
                break;
        }

        errorResponse.StatusCode = response.StatusCode;

        var json = JsonConvert.SerializeObject(errorResponse);

        await response.WriteAsync(json);
    }

    private class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
    }
}
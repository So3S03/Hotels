using Hotels.Shared.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.APIs.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
			try
			{
                await next(context);
            }
			catch (Exception ex)
			{
                var statusCode = ex switch
                {
                    BadRequest400Exception => StatusCodes.Status400BadRequest,
                    NotFound404Exception => StatusCodes.Status404NotFound,
                    Unauthorized401Exception => StatusCodes.Status401Unauthorized,
                    Conflict409Exception => StatusCodes.Status409Conflict,
                    MethodNotAllowed405Exception => StatusCodes.Status405MethodNotAllowed,
                    _ => StatusCodes.Status500InternalServerError
                };

                var title = ex switch
                {
                    BadRequest400Exception => "Bad Request",
                    NotFound404Exception => "Not Found",
                    Unauthorized401Exception => "Unauthorized",
                    Conflict409Exception => "Conflict",
                    MethodNotAllowed405Exception => "Method Not Allowed",
                    _ => "Server Error"
                };

                var problem = new ProblemDetails()
                {
                    Status = statusCode,
                    Title = title,
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };
                logger.LogError(ex, ex.Message, problem);
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(problem);
			}
        }
    }
}

using System.Net;
using System.Text.Json;
using WebApplication1.Exceptions;

namespace WebApplication1.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next; _logger = logger;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (AppNotFoundException ex)
            {
                await WriteProblem(ctx, HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteProblem(ctx, HttpStatusCode.InternalServerError, "Unexpected error");
            }
        }

        private static Task WriteProblem(HttpContext ctx, HttpStatusCode code, string detail)
        {
            ctx.Response.ContentType = "application/problem+json";
            ctx.Response.StatusCode = (int)code;

            var problem = new
            {
                type = "about:blank",
                title = code.ToString(),
                status = (int)code,
                detail
            };
            return ctx.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}

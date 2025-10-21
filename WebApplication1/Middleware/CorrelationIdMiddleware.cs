namespace WebApplication1.Middleware
{
    public class CorrelationIdMiddleware
    {
        private const string Header = "X-Correlation-Id";
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext ctx)
        {
            var cid = ctx.Request.Headers.TryGetValue(Header, out var value) && !string.IsNullOrWhiteSpace(value)
                ? value.ToString()
                : Guid.NewGuid().ToString();

            ctx.Response.Headers[Header] = cid;
            await _next(ctx);
        }
    }
}

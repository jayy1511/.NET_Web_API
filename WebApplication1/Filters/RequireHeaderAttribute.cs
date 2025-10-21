using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public sealed class RequireHeaderAttribute : ActionFilterAttribute
    {
        private readonly string _headerName;
        public RequireHeaderAttribute(string headerName) => _headerName = headerName;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey(_headerName))
            {
                context.Result = new BadRequestObjectResult($"Missing required header '{_headerName}'.");
            }
        }
    }
}

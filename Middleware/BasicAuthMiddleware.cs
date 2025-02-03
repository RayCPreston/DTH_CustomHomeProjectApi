using DTH.API.Services.UserServices;
using DTH.API.Utility;

namespace DTH.API.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BasicAuthMiddleware> _logger;
        private readonly IServiceProvider _serviceProvider;

        public BasicAuthMiddleware(RequestDelegate next,
            ILogger<BasicAuthMiddleware> logger,
            IServiceProvider serviceProvider)
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    string path = context.Request.Path;
                    if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
                        path.StartsWith("/favicon.ico", StringComparison.OrdinalIgnoreCase))
                    {
                        await _next(context);
                        return;
                    }
                    GetUserService getUserService = scope.ServiceProvider.GetRequiredService<GetUserService>();
                    IHeaderDictionary headers = context.Request.Headers;
                    if (!headers.AuthorizationHeaderNotNullAndValid(getUserService))
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Unauthorized");
                        return;
                    }
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in BasicAuthMiddleware.Invoke. Message{ex.Message}");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal Server Error");
                return;
            }
        }
    }
}

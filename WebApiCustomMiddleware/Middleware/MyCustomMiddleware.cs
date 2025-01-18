
namespace WebApiCustomMiddleware.Middleware;

public class MyCustomMiddleware : IMiddleware
{
    private ILogger<MyCustomMiddleware> _logger;

    public MyCustomMiddleware(ILogger<MyCustomMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Right before going to the endpoints");
        _logger.LogInformation($"Requested path: {context.Request.Path}");
        await next(context);
        _logger.LogInformation("Right after going to the endpoints");
        _logger.LogInformation($"Status code: {context.Response.StatusCode}");
    }
}

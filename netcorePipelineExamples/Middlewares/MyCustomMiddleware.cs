
namespace netcorePipelineExamples.Middlewares;

public class MyCustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("Custom Middleware : Before calling next");

        await next(context);

        await context.Response.WriteAsync("Custom Middleware: After calling next");
    }
}

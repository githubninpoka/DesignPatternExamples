
namespace netcorePipelineExamples.Middlewares;

public class MyCustomExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            context.Response.ContentType = "text/html";
            await next(context);
        }
        catch(Exception ex)
        {
            //context.Response.ContentType = "text/html"; // can't because there was already data output to the browser.
            await context.Response.WriteAsync($"<h2>error happened: {ex.Message}</h2>");
            //throw new Exception("Caught an exception in the custom middleware", ex);
        }
    }
}

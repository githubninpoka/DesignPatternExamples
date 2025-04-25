using netcorePipelineExamples.Middlewares;

namespace netcorePipelineExamples;

public class Program
{
    // creating middleware is 'simple'.
    // add a class that implement IMiddleware
    // register it in the services collection
    // use it in the correct place in the pipeline
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddTransient<MyCustomMiddleware>();
        var app = builder.Build();


        // example of a simple custom middleware (see the builder above)
        app.UseMiddleware<MyCustomMiddleware>();

        // the below is messy, but it works and shows different use cases for middleware.
        // app.Map, app.MapWhen, app.Use. I skipped app.UseWhen which is for rejoining a branch.
        // branching out
        app.Map("/otherRoute", (IApplicationBuilder appBuilder) =>
        {
            appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Alternative Middleware #1: Before calling next");

                await next(context);

                await context.Response.WriteAsync("Alternative Middleware #1: After calling next");
            });

            appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Alternative Middleware #2: Before calling next");

                await next(context);

                await context.Response.WriteAsync("Alternative Middleware #2: After calling next");
            });
        });

        // just a slightly more complex branch out
        app.MapWhen((HttpContext context) =>
        {
            if (context.Request.Path.Value.ToLower().Contains("iep"))
            {
                return true;
            }
            return false;
        },
        (IApplicationBuilder builder) =>
        {
            builder.Use(async(HttpContext context, RequestDelegate next) =>
            {

                await context.Response.WriteAsync("Alternative mapwhen Middleware #1: Before calling next");

                await next(context);

            });
        }
        );


        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("Middleware #1: Before calling next");

            await next(context);

            await context.Response.WriteAsync("Middleware #1: After calling next");
        });

        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("Middlware #2: Before calling next");

            await next(context);

            await context.Response.WriteAsync("Middleware #2: After calling next");
        }
        );

        app.Run(async(HttpContext context) =>
        {
            await context.Response.WriteAsync("Middleware #3, created as terminal middleware");
        });

        app.Run();
    }
}

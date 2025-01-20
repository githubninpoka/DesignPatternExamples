using CacheRedisBlazor.Components;

namespace CacheRedisBlazor
{
    public class Program
    {
        // redis container:
        // docker ->
        // create a container and run it: docker run --name <nameOfContainer> -p 6379:6379 -d redis
        // to get to the container prompt docker exec -it <dockerContainerName> sh
        // on the container prompt: redis-cli
        // select the first database: select 0
        // see keys: scan 0
        // lookup entire key: hgetall <key>

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                options.InstanceName = "RedisSimple_";
            });

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}

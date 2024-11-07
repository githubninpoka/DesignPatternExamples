using Serilog;

namespace SerilogDemo
{
    public class Program
    {
        // https://www.youtube.com/watch?v=_iryZxv8Rxw serilog plus seq Demo by Tim Corey
        // serilog sinq seq Nuget package added
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration() // needs Serilog.AspNetCore Nuget package
                .ReadFrom.Configuration(configuration) // needs Serilog.Settings.Configuration Nuget package
                .CreateLogger();
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            try // just to mess with logging a bit that is already available before the services are running.
            {
                Log.Information("Application configuring started");
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddRazorPages();
                
                // configure to use Serilog. this is different for Razor template than for other asp.net templates. normally would use app.UseSerilog()
                builder.Services.AddSerilog();

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

                app.UseSerilogRequestLogging(); // add this to log a request

                app.UseRouting();

                app.UseAuthorization();

                app.MapRazorPages();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application configuration didnt work all that great.");

            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

using BlazorStatePersistency.Client.Pages;
using BlazorStatePersistency.Components;
using Blazored.LocalStorage;

namespace BlazorStatePersistency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // have to include the service in BOTH server and client side...
            builder.Services.AddBlazoredLocalStorage();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();


            // Second state management method
            builder.Services.AddSingleton<DIPerson>();

            // Third state management method
            builder.Services.AddTransient<SessionStorageContainer<Person>>();

            // Fourth state management method
            builder.Services.AddScoped<LocalStorageContainer<Person>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}

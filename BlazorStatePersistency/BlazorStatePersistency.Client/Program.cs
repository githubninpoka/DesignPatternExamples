using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using Blazored.LocalStorage;

namespace BlazorStatePersistency.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // unfortunately this does not work in webassembly
            // https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-9.0&pivots=server#aspnet-core-protected-browser-storage
            //builder.Services.AddScoped<LocalStorageContainer<Person>>();

            // Blazored Localstorage
            builder.Services.AddBlazoredLocalStorage();
            //builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

            await builder.Build().RunAsync();
        }
    }
}

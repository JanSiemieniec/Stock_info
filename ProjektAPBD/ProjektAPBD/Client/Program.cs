using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProjektAPBD.Client.Services.Implementations;
using ProjektAPBD.Client.Services.Interfaces;
using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektAPBD.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYyNjAwQDMyMzAyZTMxMmUzMFpIRDY1aTQ5QmlVOEtVbGFZdGs1TTEyU0Q5T1grMmdYOVBnd3dUeHQ3NjA9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddHttpClient("ProjektAPBD.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44308/") });
            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ProjektAPBD.ServerAPI"));
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddApiAuthorization();
            builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
            await builder.Build().RunAsync();
        }
    }
}

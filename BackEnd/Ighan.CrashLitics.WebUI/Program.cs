using Ighan.CrashLitics.WebUI.Services;
using Ighan.CrashLitics.WebUI.Utilities;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiSettings:Url"]) });

            builder.Services.AddSingleton<TokenProvider>();
            builder.Services.AddSingleton<CookieProvider>();

            builder.Services.AddScoped<ProjectService>();

            await builder.Build().RunAsync();
        }
    }
}

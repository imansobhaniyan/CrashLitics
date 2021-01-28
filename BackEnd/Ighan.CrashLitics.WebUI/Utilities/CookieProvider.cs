using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebUI.Utilities
{
    public class CookieProvider
    {
        private readonly IJSRuntime jsRuntime;

        public CookieProvider(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task SetAsync(string key, string value)
        {
            await jsRuntime.InvokeVoidAsync("setCookie", key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            return await jsRuntime.InvokeAsync<string>("getCookie", key);
        }
    }
}

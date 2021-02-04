using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebUI.Utilities
{
    public class TokenProvider
    {
        private readonly CookieProvider cookieProvider;

        public TokenProvider(CookieProvider cookieProvider)
        {
            this.cookieProvider = cookieProvider;
        }

        public async Task SetTokenAsync(string token)
        {
            await cookieProvider.SetAsync("token", token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await cookieProvider.GetAsync("token");
        }

        public async Task<bool> HasValidTokenAsync()
        {
            return !string.IsNullOrWhiteSpace(await GetTokenAsync());
        }

        public async Task ClearTokenAsync()
        {
            await cookieProvider.ClearAsync("token");
        }
    }
}

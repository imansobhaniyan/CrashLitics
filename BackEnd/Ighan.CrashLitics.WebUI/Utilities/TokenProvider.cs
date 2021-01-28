using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebUI.Utilities
{
    public class TokenProvider
    {
        private string token;

        public void SetToken(string token)
        {
            this.token = token;
        }

        public bool HasValidToken()
        {
            return !string.IsNullOrWhiteSpace(token);
        }
    }
}

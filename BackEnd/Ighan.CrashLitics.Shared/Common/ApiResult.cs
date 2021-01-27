using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Ighan.CrashLitics.Shared.Common
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}

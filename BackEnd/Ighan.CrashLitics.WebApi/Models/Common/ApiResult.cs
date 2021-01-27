using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebApi.Models.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}

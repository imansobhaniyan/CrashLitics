using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebApi.Models.ExceptionModels
{
    public class ExceptionModel
    {
        public string Token { get; set; }

        public string DeviceId { get; set; }

        public string Manufacturer { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Release { get; set; }

        public int SDK { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public List<InnerExceptionModel> InnerExceptionModels { get; set; }
    }
}

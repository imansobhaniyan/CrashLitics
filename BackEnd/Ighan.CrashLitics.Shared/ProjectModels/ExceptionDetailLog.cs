using System;

namespace Ighan.CrashLitics.Shared.ProjectModels
{
    public class ExceptionDetailLog
    {
        public long Id { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreateDate { get; set; }

        public DeviceDetail Device { get; set; }
    }
}

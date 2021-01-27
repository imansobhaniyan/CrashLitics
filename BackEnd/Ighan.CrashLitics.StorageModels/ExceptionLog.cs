using System;
using System.Collections.Generic;
using System.Text;

namespace Ighan.CrashLitics.StorageModels
{
    public class ExceptionLog
    {
        public ExceptionLog()
        {
            CreateDate = DateTime.Now;
            InnerExceptionLogs = new List<InnerExceptionLog>();
        }

        public long Id { get; set; }

        public int DeviceId { get; set; }

        public int ProjectId { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreateDate { get; set; }

        public Device Device { get; set; }

        public Project Project { get; set; }

        public List<InnerExceptionLog> InnerExceptionLogs { get; set; }
    }
}

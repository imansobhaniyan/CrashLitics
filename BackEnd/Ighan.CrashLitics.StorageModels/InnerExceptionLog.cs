namespace Ighan.CrashLitics.StorageModels
{
    public class InnerExceptionLog
    {
        public long Id { get; set; }

        public int ExceptionLogId { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public ExceptionLog ExceptionLog { get; set; }
    }
}

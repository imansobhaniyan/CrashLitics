using System.Collections.Generic;

namespace Ighan.CrashLitics.Shared.ProjectModels
{
    public class ProjectDetailResult
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Token { get; set; }

        public List<ExceptionDetailLog> ExceptionLogs { get; set; }
    }
}

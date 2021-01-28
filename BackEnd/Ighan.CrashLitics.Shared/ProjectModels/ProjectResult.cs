using System;
using System.Collections.Generic;
using System.Text;

namespace Ighan.CrashLitics.Shared.ProjectModels
{
    public class ProjectResult
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ExceptionLogsCount { get; set; }
    }
}

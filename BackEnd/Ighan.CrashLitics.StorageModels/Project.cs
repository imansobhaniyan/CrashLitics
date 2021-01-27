using System.Collections.Generic;

namespace Ighan.CrashLitics.StorageModels
{
    public class Project
    {
        public Project()
        {
            UserProjects = new List<UserProject>();
            ExceptionLogs = new List<ExceptionLog>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Token { get; set; }

        public List<UserProject> UserProjects { get; set; }

        public List<ExceptionLog> ExceptionLogs { get; set; }
    }
}

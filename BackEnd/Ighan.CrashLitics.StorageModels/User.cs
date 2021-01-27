using System;
using System.Collections.Generic;
using System.Text;

namespace Ighan.CrashLitics.StorageModels
{
    public class User
    {
        public User()
        {
            UserProjects = new List<UserProject>();
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<UserProject> UserProjects { get; set; }
    }
}

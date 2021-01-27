using Ighan.CrashLitics.StorageModels;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Ighan.CrashLitics.DataAccessLayer
{
    public class CrashLiticsDbContext : DbContext
    {
        public CrashLiticsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<ExceptionLog> ExceptionLogs { get; set; }

        public DbSet<InnerExceptionLog> InnerExceptionLogs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<UserProject> UserProjects { get; set; }
    }
}

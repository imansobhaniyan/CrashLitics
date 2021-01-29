using Ighan.CrashLitics.DataAccessLayer;
using Ighan.CrashLitics.Shared.Common;
using Ighan.CrashLitics.Shared.ProjectModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly CrashLiticsDbContext dbContext;

        public ProjectController(CrashLiticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ApiResult<List<ProjectResult>>> Get()
        {
            try
            {
                var token = Request.Headers["token"];

                if (token == StringValues.Empty)
                    throw new Exception(ApiResult.InvalidTokenErrorMessage);

                var data = await dbContext.Projects.Where(f => f.UserProjects.Any(x => x.User.Token == token.ToString())).Select(f => new
                {
                    f.Id,
                    f.Title,
                    ExceptionLogsCount = f.ExceptionLogs.Count
                }).ToListAsync();

                return new ApiResult<List<ProjectResult>>
                {
                    Success = true,
                    Data = data.ConvertAll(f => new ProjectResult
                    {
                        ExceptionLogsCount = f.ExceptionLogsCount,
                        Id = f.Id,
                        Title = f.Title
                    })
                };
            }
            catch (Exception exception)
            {
                return new ApiResult<List<ProjectResult>>
                {
                    ErrorMessage = exception.Message
                };
            }
        }

        [HttpPost]
        public async Task<ApiResult<ProjectResult>> Post(ProjectModel model)
        {
            try
            {
                var token = Request.Headers["token"];
                if (token == StringValues.Empty)
                    throw new Exception(ApiResult.InvalidTokenErrorMessage);

                var project = await dbContext.Projects.FirstOrDefaultAsync(f => f.Id == model.Id);
                if (project == null)
                {
                    project = new StorageModels.Project();

                    var user = await dbContext.Users.FirstOrDefaultAsync(f => f.Token == token.ToString());

                    project.UserProjects.Add(new StorageModels.UserProject
                    {
                        User = user
                    });

                    await dbContext.Projects.AddAsync(project);
                }

                project.Title = model.Title;

                await dbContext.SaveChangesAsync();

                return new ApiResult<ProjectResult>
                {
                    Success = true,
                    Data = new ProjectResult
                    {
                        Id = project.Id,
                        Title = project.Title,
                        ExceptionLogsCount = await dbContext.ExceptionLogs.CountAsync(f => f.ProjectId == project.Id)
                    }
                };
            }
            catch (Exception exception)
            {
                return new ApiResult<ProjectResult>
                {
                    ErrorMessage = exception.Message
                };
            }
        }
    }
}

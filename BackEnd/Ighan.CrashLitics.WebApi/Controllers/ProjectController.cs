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
    }
}

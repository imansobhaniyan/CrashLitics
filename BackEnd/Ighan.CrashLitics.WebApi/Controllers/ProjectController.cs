using Ighan.CrashLitics.DataAccessLayer;
using Ighan.CrashLitics.Shared.Common;
using Ighan.CrashLitics.Shared.ProjectModels;
using Ighan.SimpleMapper.Core;

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

        private readonly static Random rand = new Random();

        public ProjectController(CrashLiticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<ProjectDetailResult>> Get(int id)
        {
            try
            {
                var token = Request.Headers["token"];
                if (token == StringValues.Empty)
                    throw new Exception(ApiResult.InvalidTokenErrorMessage);

                var project = await dbContext.Projects
                    .Include(f => f.ExceptionLogs)
                        .ThenInclude(f => f.Device)
                        .ThenInclude(f => f.Model)
                        .ThenInclude(f => f.Brand)
                        .ThenInclude(f => f.Manufacturer)
                    .FirstOrDefaultAsync(f => f.Id == id);

                var data = Mapper.Map<ProjectDetailResult>(project);

                return new ApiResult<ProjectDetailResult>
                {
                    Success = true,
                    Data = data
                };
            }
            catch (Exception exception)
            {
                return new ApiResult<ProjectDetailResult>
                {
                    ErrorMessage = exception.Message
                };
            }
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
                    project = new StorageModels.Project
                    {
                        Token = await GetUniqeTokenAsync()
                    };

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

        private async Task<string> GetUniqeTokenAsync()
        {
            string token;

            do
            {
                token = string.Join("", Enumerable.Range(0, 15).Select(f => (char)rand.Next((int)'a', (int)'z')));
            }
            while (await dbContext.Projects.AnyAsync(f => f.Token == token));

            return token;
        }
    }
}

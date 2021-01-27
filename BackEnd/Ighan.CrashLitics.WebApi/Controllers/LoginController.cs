using Ighan.CrashLitics.DataAccessLayer;
using Ighan.CrashLitics.Shared.Common;
using Ighan.CrashLitics.Shared.LoginModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly CrashLiticsDbContext dbContext;

        public LoginController(CrashLiticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ApiResult<LoginResult>> Post(LoginModel model)
        {
            try
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(f => f.UserName == model.UserName && f.Password == model.Password);

                return new ApiResult<LoginResult>
                {
                    Success = true,
                    Data = new LoginResult
                    {
                        Success = user != null
                    }
                };
            }
            catch (Exception exception)
            {
                return new ApiResult<LoginResult>
                {
                    ErrorMessage = exception.Message
                };
            }
        }
    }
}

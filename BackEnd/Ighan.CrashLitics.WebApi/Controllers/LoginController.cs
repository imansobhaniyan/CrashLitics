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

        private readonly static Random rand = new Random();

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

                if (user != null && string.IsNullOrWhiteSpace(user.Token))
                {

                    user.Token = await GetUniqeTokenAsync();
                    await dbContext.SaveChangesAsync();
                }

                return new ApiResult<LoginResult>
                {
                    Success = true,
                    Data = new LoginResult
                    {
                        Success = user != null,
                        Token = user?.Token
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

        private async Task<string> GetUniqeTokenAsync()
        {
            string token;

            do
            {
                token = string.Join("", Enumerable.Range(0, 255).Select(f => (char)rand.Next((int)'A', (int)'z')));
            }
            while (await dbContext.Users.AnyAsync(f => f.Token == token));

            return token;
        }
    }
}

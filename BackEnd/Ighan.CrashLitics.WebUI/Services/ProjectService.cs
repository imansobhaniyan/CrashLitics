using Ighan.CrashLitics.Shared.Common;
using Ighan.CrashLitics.Shared.ProjectModels;
using Ighan.CrashLitics.WebUI.Utilities;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Ighan.CrashLitics.WebUI.Services
{
    public class ProjectService : BaseService
    {
        protected override string ApiAddress { get { return "/api/project"; } }

        private List<ProjectResult> projects;

        public ProjectService(HttpClient httpClient, TokenProvider tokenProvider, NavigationManager navigationManager)
            : base(httpClient, tokenProvider, navigationManager)
        {
        }

        public async Task<List<ProjectResult>> GetAllAsync()
        {
            if (projects == null)
                projects = await GetListAsync<ProjectResult>();

            return projects;
        }
    }
}

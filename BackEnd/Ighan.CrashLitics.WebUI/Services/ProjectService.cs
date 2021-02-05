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
        public delegate void ProjectAddedEventHandler(ProjectResult projectResult);

        public event ProjectAddedEventHandler OnProjectAdded;

        protected override string ApiAddress { get { return "/api/project"; } }

        private List<ProjectResult> projects;

        public ProjectService(HttpClient httpClient, TokenProvider tokenProvider, NavigationManager navigationManager)
            : base(httpClient, tokenProvider, navigationManager)
        {
            OnProjectAdded += (newProject) => projects.Add(newProject);
        }

        public async Task<List<ProjectResult>> GetAllAsync()
        {
            if (projects == null)
                projects = await GetListAsync<ProjectResult>();

            return projects;
        }

        public async Task<ProjectDetailResult> GetById(int projectId)
        {
            return await GetAsync<ProjectDetailResult>(projectId);
        }

        public async Task<ProjectResult> AddProjectAsync(string title)
        {
            var newProject = await PostAsync<ProjectResult, ProjectModel>(new ProjectModel { Title = title });
            OnProjectAdded.Invoke(newProject);
            return newProject;
        }
    }
}

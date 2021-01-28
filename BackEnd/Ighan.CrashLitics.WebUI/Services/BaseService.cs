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
    public abstract class BaseService
    {
        protected abstract string ApiAddress { get; }

        private readonly HttpClient httpClient;

        private readonly TokenProvider tokenProvider;

        private readonly NavigationManager navigationManager;

        public BaseService(HttpClient httpClient, TokenProvider tokenProvider, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.tokenProvider = tokenProvider;
            this.navigationManager = navigationManager;
        }

        protected async Task<HttpClient> GetHttpClientAsync()
        {
            if (await tokenProvider.HasValidTokenAsync())
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("token", await tokenProvider.GetTokenAsync());
            return httpClient;
        }

        protected async Task<T> GetAsync<T>()
        {
            var response = await (await GetHttpClientAsync()).GetFromJsonAsync<ApiResult<T>>(ApiAddress);

            if (!response.Success)
                if (response.ErrorMessage == ApiResult.InvalidTokenErrorMessage)
                {
                    navigationManager.NavigateTo("/login");
                    return default;
                }
                else
                    throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        protected async Task<List<T>> GetListAsync<T>()
        {
            return await GetAsync<List<T>>();
        }
    }
}

using EvoHub.Domain.Interfaces;
using EvoHub.Domain;
using Newtonsoft.Json;
using RestSharp;
using EvoHub.Infra.CrossCutting;

namespace EvoHub.Infra.ApiGitHub
{
    public class GitHubApi : IGitHubApi
    {
        private readonly RestClient _client;

        public GitHubApi(RestClient client)
        {
            _client = client;
        }

        private async Task<ActionResult<TModel>> FetchFromGitHubApi<TModel>(string url, string errorMessage) where TModel : class, new()
        {
            var request = new RestRequest(url, Method.Get).AddHeader("User-Agent", "EvoHub");

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            {
                return new ActionResult<TModel>()
                {
                    IsValid = false,
                    Message = errorMessage,
                    Result = new TModel()
                };
            }

            TModel result = JsonConvert.DeserializeObject<TModel>(response.Content) ?? new TModel();

            return new ActionResult<TModel>()
            {
                IsValid = true,
                Message = Constants.SucessRepositoriesRetrieved,
                Result = result
            };
        }

        public async Task<ActionResult<List<GitHubRepository>>> GetRepository(string owner)
        {
            string url = $"https://api.github.com/users/{owner}/repos";
            string errorMessage = Constants.ErrorCouldntRetrieveUserRepositories;

            return await FetchFromGitHubApi<List<GitHubRepository>>(url, errorMessage);
        }

        public async Task<ActionResult<RepositoryModel>> GetRepositoriesByName(string name)
        {
            string url = $"https://api.github.com/search/repositories?q={name}";
            string errorMessage = Constants.ErrorCouldntRetrieveRepositoriesRelatedToName;

            return await FetchFromGitHubApi<RepositoryModel>(url, errorMessage);
        }
    }
}

using EvoHub.Business.Contract;
using EvoHub.Domain;
using EvoHub.Domain.Interfaces;
using EvoHub.Infra.CrossCutting;

namespace EvoHub.Business.Services
{
    public class GitHubApiBusiness : IGitHubApiBusiness
    {
        private readonly IGitHubApi _gitHubApi;

        public GitHubApiBusiness(IGitHubApi gitHubApi)
        {
            _gitHubApi = gitHubApi;
        }

        public async Task<ActionResult<List<GitHubRepositoryViewModel>>> GetDefaultOwnerRepositories()
        {
            var result = new ActionResult<List<GitHubRepositoryViewModel>>();

            var apiResult = await _gitHubApi.GetRepository(Constants.DefaultOwner);

            if (!apiResult.IsValid)
            {
                result.IsValid = false;
                result.Message = apiResult.Message;
                return result;
            }

            result.Result = apiResult.Result.Select(repo => new GitHubRepositoryViewModel
            {
                Id = repo.Id,
                Name = repo.Name,
                FullName = repo.FullName,
                Owner = repo.Owner,
                Description = repo.Description,
                Url = repo.HtmlUrl,
                UpdatedAt = repo.UpdatedAt,
                Language = repo.Language
            }).ToList();

            result.IsValid = true;
            result.Message = apiResult.Message;


            return result;
        }

        public async Task<ActionResult<GitHubRepositoryViewModel>> GetOwnerRepository(string owner, long id)
        {
            var result = new ActionResult<GitHubRepositoryViewModel>();

            var apiResult = await _gitHubApi.GetRepository(owner);

            if (!apiResult.IsValid)
            {
                result.IsValid = false;
                result.Message = apiResult.Message;
                return result;
            }

            var repo = apiResult.Result.FirstOrDefault(r => r.Id == id);

            if (repo == null)
            {
                result.IsValid = false;
                result.Message = Constants.ErrorRepositoryNotFound;
                return result;
            }

            result.Result = new GitHubRepositoryViewModel
            {
                Id = repo.Id,
                Name = repo.Name,
                FullName = repo.FullName,
                Owner = repo.Owner,
                Description = repo.Description,
                Url = repo.HtmlUrl,
                UpdatedAt = repo.UpdatedAt,
                Language = repo.Language
            };

            result.IsValid = true;
            result.Message = Constants.SucessRepositoriesRetrieved;

            return result;
        }

        public async Task<ActionResult<List<GitHubRepositoryViewModel>>> GetRepositoriesByName(string name)
        {
            var result = new ActionResult<List<GitHubRepositoryViewModel>>();

            var apiResult = await _gitHubApi.GetRepositoriesByName(name);

            if (!apiResult.IsValid)
            {
                result.IsValid = false;
                result.Message = apiResult.Message;
                return result;
            }

            result.Result = apiResult.Result.Repositories.Select(repo => new GitHubRepositoryViewModel
            {
                Id = repo.Id,
                Name = repo.Name,
                FullName = repo.FullName,
                Owner = repo.Owner,
                Description = repo.Description,
                Url = repo.HtmlUrl,
                UpdatedAt = repo.UpdatedAt,
                Language = repo.Language
            }).ToList();

            result.IsValid = true;
            result.Message = Constants.SucessRepositoriesRetrieved;


            return result;
        }
    }
}

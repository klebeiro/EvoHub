using EvoHub.Domain;

namespace EvoHub.Business.Contract
{
    public interface IGitHubApiBusiness
    {
        Task<ActionResult<List<GitHubRepositoryViewModel>>> GetDefaultOwnerRepositories();

        Task<ActionResult<GitHubRepositoryViewModel>> GetOwnerRepository(string owner, long id);

        Task<ActionResult<List<GitHubRepositoryViewModel>>> GetRepositoriesByName(string name);
    }
}
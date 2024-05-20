namespace EvoHub.Domain.Interfaces
{
    public interface IGitHubApi
    {
        Task<ActionResult<List<GitHubRepository>>> GetRepository(string owner);

        Task<ActionResult<RepositoryModel>> GetRepositoriesByName(string name);
    }
}

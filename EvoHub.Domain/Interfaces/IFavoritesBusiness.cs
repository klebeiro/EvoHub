using EvoHub.Domain;

namespace EvoHub.Domain.Interfaces
{
    public interface IFavoritesBusiness
    {
        Task<ActionResult<List<FavoriteViewModel>>> GetFavoriteRepositories();

        Task<ActionResult<FavoriteViewModel>> SaveRepositoryInFavorites(long id);

        Task<ActionResult<FavoriteViewModel>> RemoveRepositoryFromFavorites(long id);
    }
}

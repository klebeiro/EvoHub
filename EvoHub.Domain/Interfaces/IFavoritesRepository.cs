using EvoHub.Domain;

namespace EvoHub.Infra.Contract
{
    public interface IFavoritesRepository
    {
        Task<bool> ExistsByCheckAlreadyAsync(Favorite favorite);

        Task<List<Favorite>> GetAllAsync();

        Task<bool> InsertAsync(Favorite favorite);

        Task<bool> RemoveAsync(Favorite favorite);
    }
}

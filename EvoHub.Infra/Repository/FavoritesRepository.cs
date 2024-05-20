using EvoHub.Domain;
using EvoHub.Infra.Contract;
using EvoHub.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace EvoHub.Infra.Repository
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly DatabaseContext _dbContext;

        public FavoritesRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByCheckAlreadyAsync(Favorite favorite)
        {
            return await _dbContext.Favorites.AnyAsync(f => f.Id == favorite.Id);
        }

        public async Task<List<Favorite>> GetAllAsync()
        {
            return await _dbContext.Favorites.ToListAsync();
        }

        public async Task<bool> InsertAsync(Favorite favorite)
        {

            await _dbContext.Favorites.AddAsync(favorite);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAsync(Favorite favorite)
        {
            _dbContext.Favorites.Remove(favorite);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

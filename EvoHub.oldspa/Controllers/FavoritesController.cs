using EvoHub.Domain;
using EvoHub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvoHub.SPA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesBusiness _favoritesBusiness;

        public FavoritesController(IFavoritesBusiness favoritesBusiness)
        {
            _favoritesBusiness = favoritesBusiness;
        }

        [HttpGet("/favorites")]
        public async Task<ActionResult> GetFavoriteRepositories()
        {
            var result = await _favoritesBusiness.GetFavoriteRepositories();

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("/favorites")]
        public async Task<ActionResult> SaveRepositoryInFavorites(FavoriteViewModel favorite)
        {
            var result = await _favoritesBusiness.SaveRepositoryInFavorites(favorite);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("/favorites")]
        public async Task<ActionResult> RemoveRepositoryFromFavorites(FavoriteViewModel favorite)
        {
            var result = await _favoritesBusiness.RemoveRepositoryFromFavorites(favorite);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
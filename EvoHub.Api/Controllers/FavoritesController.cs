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

        [HttpGet]
        public async Task<ActionResult> GetFavoriteRepositories()
        {
            var result = await _favoritesBusiness.GetFavoriteRepositories();

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SaveRepositoryInFavorites([FromQuery] long id)
        {
            var result = await _favoritesBusiness.SaveRepositoryInFavorites(id);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveRepositoryFromFavorites([FromQuery] long id)
        {
            var result = await _favoritesBusiness.RemoveRepositoryFromFavorites(id);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

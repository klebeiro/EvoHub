using Microsoft.AspNetCore.Mvc;
using EvoHub.Business.Contract;

namespace EvoHub.SPA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubApiController : ControllerBase
    {
        private readonly IGitHubApiBusiness _gitHubApiBusiness;

        public GitHubApiController(IGitHubApiBusiness gitHubApiBusiness)
        {
            _gitHubApiBusiness = gitHubApiBusiness;
        }

        [HttpGet("repositories")]
        public async Task<ActionResult> GetDefaultOwnerRepositories()
        {
            var result = await _gitHubApiBusiness.GetDefaultOwnerRepositories();

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("owner-repository-by-id")]
        public async Task<ActionResult> GetOwnerRepository([FromQuery] string owner, [FromQuery] long id)
        {
            var result = await _gitHubApiBusiness.GetOwnerRepository(owner, id);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("repositories-by-name")]
        public async Task<ActionResult> GetRepositoriesByName([FromQuery] string name)
        {
            var result = await _gitHubApiBusiness.GetRepositoriesByName(name);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

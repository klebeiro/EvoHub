using Microsoft.AspNetCore.Mvc;
using EvoHub.Business.Contract;

namespace EvoHub.App.Controllers
{
    public class GitHubApiController : ControllerBase
    {
        private readonly IGitHubApiBusiness _gitHubApiBusiness;

        public GitHubApiController(IGitHubApiBusiness gitHubApiBusiness)
        {
            _gitHubApiBusiness = gitHubApiBusiness;
        }

        [HttpGet("/get-repositories")]
        public async Task<ActionResult> GetDefaultOwnerRepositories()
        {
            var result = await _gitHubApiBusiness.GetDefaultOwnerRepositories();

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("/get-repositories-by-owner")]
        public async Task<ActionResult> GetOwnerRepository([FromQuery] string owner, [FromQuery] long id)
        {
            var result = await _gitHubApiBusiness.GetOwnerRepository(owner, id);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("/get-repositories-by-name")]
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
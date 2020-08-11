using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp.Core.Instagram.Service;
using System.Threading.Tasks;

namespace PuppeteerSharpWithVuejs.Controllers
{
    [Route("api/instagram")]
    [ApiController]
    public sealed class InstagramApiController : ControllerBase
    {
        private readonly IInstagramService _instagramService;

        public InstagramApiController(
            IInstagramService instagramService)
        {
            _instagramService = instagramService;
        }

        [HttpGet("{profile}")]
        public async Task<ActionResult> GetPostsAsync(
            string profile = "nasa")
        {
            var result = await _instagramService.GetLastsPostsAsync(profile);

            return Ok(new {
                success = result != null,
                data = result
            });
        }
    }
}

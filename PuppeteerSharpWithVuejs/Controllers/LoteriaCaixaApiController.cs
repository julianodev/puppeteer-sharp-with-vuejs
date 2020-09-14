using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp.Core.Instagram.Service;
using PuppeteerSharp.Core.Service.LoteriaCaixa;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PuppeteerSharpWithVuejs.Controllers
{
    [Route("api/loteria-caixa")]
    [ApiController]
    public sealed class LoteriaCaixaApiController : ControllerBase
    {
        private readonly ILoteriaCaixaService _loteriaCaixaService;

        public LoteriaCaixaApiController(
            ILoteriaCaixaService loteriaCaixaService
          )
        {
            _loteriaCaixaService = loteriaCaixaService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetPostsAsync(
            string date = null,
            string contest = null)
        {
            var result = await _loteriaCaixaService.GetLastDrawByCrawling();
            var success = result != null;

            if (date != null)
            {
                return Ok(new
                {
                    success = success,
                    data = result?.FirstOrDefault(x => x.DataSorteio == date)
                });
            }

            return Ok(new
            {
                success = success,
                data = result
            });
        }
    }
}

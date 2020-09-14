using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PuppeteerSharp.Core.LoteriaCaixa.Models;

namespace PuppeteerSharp.Core.Service.LoteriaCaixa
{
    public sealed class LoteriaCaixaService : ILoteriaCaixaService
    {
        private const string _pageLoteriaCaixaUrl = "urlApiLoteriaCaixa";
        private readonly IConfiguration _configuration;

        public LoteriaCaixaService(
            IConfiguration configuration)
            => _configuration = configuration;


        public async Task<IEnumerable<Sorteio>> GetLastDrawByCrawling(
            string date = null,
            string contest = null)
        {
            IEnumerable<Sorteio> result = null;

            try
            {
                var browserFetcher = Puppeteer.CreateBrowserFetcher(new BrowserFetcherOptions());

                var revisionInfo = await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);

                var optionsLaunch = new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = revisionInfo.ExecutablePath
                };

                using (var browser = await Puppeteer.LaunchAsync(optionsLaunch))
                using (var page = await browser.NewPageAsync())
                {
                    var pageUrl = _configuration.GetSection(_pageLoteriaCaixaUrl).Value;

                    await page.GoToAsync(pageUrl);

                    var jsSelectAllAnchors = @"() => {
                                                    const table = document.querySelector('table');
                                                    let items = [];

                                                    const headers = {
                                                        0: 'concurso',
                                                        1:'dataSorteio',
                                                        2:'premio1',
                                                        3:'premio2',
                                                        4:'premio3',
                                                        5:'premio4',
                                                        6:'premio5',
                                                        7:'valPremio1',
                                                        8:'valPremio2',
                                                        9:'valPremio3',
                                                        10:'valPremio4',
                                                        11:'valPremio5'
                                                    }

                                                    if (table) {
                                                        Array.from(table.rows).forEach((tr, row_ind) => {
                                                           if(row_ind !== 0) {
                                                             let obj = {};
                                                            Array.from(tr.cells).forEach((cell, col_ind) => {
                                                               obj[headers[col_ind]] = cell.textContent;
                                                            });
                                                            items.push(obj);
                                                           }
                                                        });
                                                    }

                                                   return items;
                                                }";

                    var items = await page.EvaluateFunctionAsync<Sorteio[]>(jsSelectAllAnchors);

                    result = items;

                    await page.CloseAsync();
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}

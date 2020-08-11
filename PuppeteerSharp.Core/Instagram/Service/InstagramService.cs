using PuppeteerSharp.Core.Instagram.Models;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuppeteerSharp.Core.Instagram.Service
{
    public sealed class InstagramService : IInstagramService
    {

        public async Task<IEnumerable<Image>> GetLastsPostsAsync(
            string profile)
        {
            IEnumerable<Image> result = null;

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
                    var profileUrl = $"https://www.instagram.com/{profile}/";

                    await page.GoToAsync(profileUrl);

                    var jsSelectAllAnchors = @"Array.from(document.querySelectorAll('article a')).map(el => {
                                                const url = el.href;

                                                const child = el.firstChild.firstChild.firstChild

                                                return {
                                                            link: url, 
                                                            description: child.alt, 
                                                            src: child.src
                                                       }
                                            })";

                    var images = await page.EvaluateExpressionAsync<Image[]>(jsSelectAllAnchors);

                    result = images;

                   await page.CloseAsync();
                }
            }
            catch 
            {

            }

            return result;
        }
    }
}

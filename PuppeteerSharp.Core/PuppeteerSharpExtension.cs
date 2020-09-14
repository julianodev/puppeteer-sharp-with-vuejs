using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp.Core.Instagram;
using PuppeteerSharp.Core.LoteriaCaixa;

namespace PuppeteerSharp.Core
{
    public static class PuppeteerSharpExtension
    {
        public static void AddPuppeteerSharp(
           this IServiceCollection service)
        {
            service.AddInstagram();
            service.AddLoteriaCaixa();
        }
    }
}

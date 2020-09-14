using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp.Core.Service.LoteriaCaixa;

namespace PuppeteerSharp.Core.LoteriaCaixa
{
    public static class LoteriaCaixaExtension
    {

        public static void AddLoteriaCaixa(
            this IServiceCollection service)
        {
            service.AddSingleton<ILoteriaCaixaService, LoteriaCaixaService>();
        }
    }
}

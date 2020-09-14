using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp.Core.Instagram.Service;

namespace PuppeteerSharp.Core.Instagram
{
    public static class LoteriaCaixaExtension
    {

        public static void AddInstagram(
            this IServiceCollection service)
        {
            service.AddSingleton<IInstagramService, InstagramService>();
        }
    }
}

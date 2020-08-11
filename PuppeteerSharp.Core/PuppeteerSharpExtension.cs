using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp.Core.Instagram;

namespace PuppeteerSharp.Core
{
    public static class PuppeteerSharpExtension
    {
        public static void AddPuppeteerSharp(
           this IServiceCollection service)
        {
            service.AddInstagram();
        }
    }
}

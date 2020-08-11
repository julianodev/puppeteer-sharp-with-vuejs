using PuppeteerSharp.Core.Instagram.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PuppeteerSharp.Core.Instagram.Service
{
    public interface IInstagramService
    {
        Task<IEnumerable<Image>> GetLastsPostsAsync(
            string profile);
    }
}

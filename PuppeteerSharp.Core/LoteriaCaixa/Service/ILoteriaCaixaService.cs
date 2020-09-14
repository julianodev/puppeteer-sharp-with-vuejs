using PuppeteerSharp.Core.LoteriaCaixa.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuppeteerSharp.Core.Service.LoteriaCaixa
{
    public interface ILoteriaCaixaService
    {
        Task<IEnumerable<Sorteio>> GetLastDrawByCrawling(
            string date = null,
            string contest = null);
    }
}

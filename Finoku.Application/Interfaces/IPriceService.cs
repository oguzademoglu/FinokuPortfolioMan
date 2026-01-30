using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Application.Interfaces
{
    public interface IPriceService
    {
        Task<decimal> GetCurrentPriceAsync(string assetName, string categoryName);
    }
}

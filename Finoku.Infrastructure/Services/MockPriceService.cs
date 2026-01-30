using Finoku.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Infrastructure.Services
{
    public class MockPriceService : IPriceService
    {
        public Task<decimal> GetCurrentPriceAsync(string assetName, string categoryName)
        {
            // 1. Eğer varlık "Cash" kategorisindeyse, birim fiyat her zaman 1
            if (categoryName.ToLower().Contains("cash"))
            {
                return Task.FromResult(1.0m);
            }

            // 2. Diğer varlıklar için isme göre fiyat belirleme //
            decimal price = assetName.ToLower() switch
            {
                "apple" => 185.50m,
                "bitcoin" => 42000.00m,
                "ethereum" => 2500.00m,
                "gold" => 2050.00m,
                _ => 100.00m
            };

            return Task.FromResult(price);
        }
    }
}

using Finoku.Application.DTOs;
using Finoku.Application.Interfaces;
using Finoku.Domain.Entities;
using Finoku.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finoku.Infrastructure.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;


        public ExchangeRateService(HttpClient httpClient, AppDbContext context, IConfiguration config)
        {
            _httpClient = httpClient;
            _context = context;
            _config = config;
        }


        //GET https://v6.exchangerate-api.com/v6/YOUR-API-KEY/pair/EUR/GBP
        //GET https://v6.exchangerate-api.com/v6/YOUR-API-KEY/pair/EUR/GBP/AMOUNT

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var pair = $"{fromCurrency}-{toCurrency}";
            var dbRate = await _context.ExchangeRates
                                .FirstOrDefaultAsync(rate => rate.CurrencyPair == pair && rate.LasyUpdated == DateTime.Today);

            if (dbRate != null)
            {
                return dbRate.Rate;
            }

            // if(dbRate == null) 
            var apiKey = _config["ExchangeRateApi:ApiKey"];
            var baseUrl = _config["ExchangeRateApi:BaseUrl"];
            //GET https://v6.exchangerate-api.com/v6/YOUR-API-KEY/pair/EUR/GBP
            //GET https://v6.exchangerate-api.com/v6/YOUR-API-KEY/pair/EUR/GBP/AMOUNT
            var url = $"{baseUrl}{apiKey}/pair/{fromCurrency}/{toCurrency}";
            //var response = await _httpClient.GetAsync(url);
            var response = await _httpClient.GetFromJsonAsync<ExchangeRateDto>(url);

            if (response != null && response.result == "success") 
            {
                // var olan bir pair yoksa ekleme
                // varsa rate'i güncelleme
                var existing = await _context.ExchangeRates.FirstOrDefaultAsync(r => r.CurrencyPair == pair);
                if (existing != null)
                {
                    existing.Rate = response.conversion_rate;
                    existing.LasyUpdated = DateTime.Now;
                }
                else
                {
                    _context.ExchangeRates.Add(new ExchangeRate
                    {
                        CurrencyPair = pair,
                        Rate = response.conversion_rate,
                        LasyUpdated = DateTime.Now
                    });
                }
                await _context.SaveChangesAsync();
                return response.conversion_rate;
            }
            return 1m; // HATA DURUMUNDA!
        }


        // rates should be updatable daily
        public async Task SyncRatesAsync()
        {
            var baseCurrencies = new[] {"USD", "EUR", "TRY", "GBP" };
            foreach (var fromCurrency in baseCurrencies)
            {
                foreach (var toCurrency in baseCurrencies)
                {
                    if (fromCurrency == toCurrency) continue;
                    await GetExchangeRateAsync(fromCurrency, toCurrency);
                }
            }
        }
    }
}

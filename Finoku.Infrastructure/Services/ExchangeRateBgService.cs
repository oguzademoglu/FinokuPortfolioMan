using Finoku.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;

namespace Finoku.Infrastructure.Services
{
    public class ExchangeRateBgService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ExchangeRateBgService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var exchangeService = scope.ServiceProvider.GetRequiredService<IExchangeRateService>();

                    // İşlemini burada yap (5 dakikada bir çalışacak şekilde)
                    Console.WriteLine($"[LOG] Kur güncelleme işlemi başladı: {DateTime.Now}");
                }

                // 5 dakika bekle
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
}
}

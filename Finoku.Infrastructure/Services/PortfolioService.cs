using Finoku.Application.DTOs;
using Finoku.Application.Interfaces;
using Finoku.Domain.Entities;
using Finoku.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Infrastructure.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly AppDbContext _context;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IPriceService _priceService;

        public PortfolioService(AppDbContext context, IExchangeRateService exchangeRateService, IPriceService priceService)
        {
            _context = context;
            _exchangeRateService = exchangeRateService;
            _priceService = priceService;
        }
        public async Task AddAsset(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            // Mongo log buraya gelecek
        }

        public async Task DeleteAsset(int assetId, int userId)
        {
            var asset = _context.Assets.FirstOrDefault(a => a.Id == assetId && a.UserId == userId);
            if (asset == null)
            {
                //throw new Exception(new {Message = "Başarısız"});  -> hatalı.s
                throw new KeyNotFoundException("Silinmek istenen varlık bulunamadı."); // AI'in önerisi
            }
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Asset>> GetUserPorfolio(int userId, int? categoryId = null)
        {
            var assets = _context.Assets.Where(asset => asset.UserId == userId);
            if (categoryId.HasValue)
            {
                assets = assets.Where(asset => asset.AssetCategoryId == categoryId);
            }
            return await assets.ToListAsync();
        }

        public async Task<List<Asset>> GetAllPortfolios()
        {
            return await _context.Assets.Include(asset => asset.User).ToListAsync();
        }

        public async Task<PortfolioReportDto> GetPortfolioReportAsync(int userId, string targetCurrency)
        {
            var assets = await _context.Assets.Where(asset => asset.UserId == userId).ToListAsync();
            if (assets == null || assets.Count == 0) 
            {
                return new PortfolioReportDto(0, 0, 0, targetCurrency, new List<AssetDetailDto>());
            }
            decimal totalValue = 0;
            decimal totalCost = 0;

            var details = new List<AssetDetailDto>();

            foreach (var asset in assets) 
            { 
                var rate = await _exchangeRateService.GetExchangeRateAsync(asset.Currency, targetCurrency);

                var currentUnitPrice = await _priceService.GetCurrentPriceAsync(asset.Name, asset.Category?.Name ?? "");

                decimal currentValue = asset.Amount * currentUnitPrice * rate;
                decimal costValue = asset.Amount * asset.PurchasePrice * rate;
                decimal profitLoss = currentValue - costValue;

                totalValue += currentValue;
                totalCost += costValue;

                details.Add(new AssetDetailDto(asset.Name, asset.Amount, currentValue, profitLoss));
            }

            decimal totalProfitLoss = totalValue - totalCost;
            decimal percentageOfProfitLoss = totalCost != 0 ? (totalProfitLoss/totalCost) * 100 : 0;

            return new PortfolioReportDto(totalValue, totalProfitLoss, percentageOfProfitLoss, targetCurrency, details);
        }

        
    }
}

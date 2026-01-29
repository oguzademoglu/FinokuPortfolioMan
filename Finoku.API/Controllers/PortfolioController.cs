using Finoku.Application.DTOs;
using Finoku.Application.Interfaces;
using Finoku.Domain.Entities;
using Finoku.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finoku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateAssetDto dto) 
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if(userId == null) throw new KeyNotFoundException("Kullanıcı bulunamadı");  // portfolioService DeleteAsset()'den kopya
            var asset = new Asset
            {
                Name = dto.Name,
                Amount = dto.Amount,
                Currency = dto.Currency,
                PurchasePrice = dto.PurchasePrice,
                PurchasedAt = DateTime.UtcNow,
                UserId = userId
            };
            _portfolioService.AddAsset(asset);
            return Ok(new { message = "Varlık Başarıyla Eklendi" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) throw new KeyNotFoundException("Kullanıcı bulunamadı");
            await _portfolioService.DeleteAsset(id, userId);
            return Ok(new { message = "Varlık başarıyla silindi." });
        }


        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPortfolios()
        {
            var portfolios = await _portfolioService.GetAllPortfolios();
            return Ok(portfolios);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPortfolioAsync(int? categoryId = null)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == null) throw new KeyNotFoundException("Kullanıcı bulunamadı");
            var result = await _portfolioService.GetUserPorfolio(userId, categoryId);

            return Ok(result);
        }


        [HttpGet("report")]
        public async Task<IActionResult> GetReport(string targetCurrency)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == null) throw new KeyNotFoundException("Kullanıcı bulunamadı");
            var report = await _portfolioService.GetPortfolioReportAsync(userId, targetCurrency.ToUpper()); // currency kesilikle büyük harf olmalı!
            return Ok(report);
        }

    }
}

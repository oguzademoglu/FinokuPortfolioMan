using Finoku.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finoku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public CurrencyController (IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRate(string from = "USD", string to = "TRY")
        {
            try 
            {
                var rate = await _exchangeRateService.GetExchangeRateAsync(from.ToUpper(), to.ToUpper());
                return Ok(new { From = from, To = to, Rate = rate, Time = DateTime.Now });
            }
            catch (Exception ex)
            { 
                return BadRequest(new { Message = "Kur bilgisi alınamadı", Error = ex.Message});
            }
            throw new NotImplementedException();
        }

        // yalnızca admin rolü yapabilir
        [Authorize(Roles = "Admin")]
        [HttpPost("sync")]
        public async Task<IActionResult> SyncRates()
        {
            try
            {
                await _exchangeRateService.SyncRatesAsync();
                return Ok(new { Message = "Kurlar başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Güncelleme başarısız.", Error = ex.Message });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Application.DTOs
{
    public record PortfolioReportDto
    (
         decimal TotalValue,         // varlıkların hedef kurdaki toplamı
         decimal TotalProfitLoss,    // Toplam Kar/Zarar
         decimal ProfitLossPercentage, // Yüzdesel Kar/Zarar
         string TargetCurrency,
         List<AssetDetailDto> Assets
    );
}

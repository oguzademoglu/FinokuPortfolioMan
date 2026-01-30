using Finoku.Application.DTOs;
using Finoku.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Application.Interfaces
{
    public interface IPortfolioService
    {
        Task<List<Asset>> GetUserPorfolio(int userId, int? categoryId = null );
        Task<List<Asset>> GetAllPortfolios(); // only Admin!
        Task AddAsset(Asset asset);
        Task DeleteAsset(int assetId, int userId);
        Task UpdateAsset(int assetId, int userId, Asset asset);
        Task<PortfolioReportDto> GetPortfolioReportAsync(int userId, string targetCurrency);
    }
}

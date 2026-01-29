using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Application.DTOs
{
    public record AssetDetailDto
    (
        string Name,
        decimal Amount,
        decimal CurrentValue,
        decimal ProfitLoss
    );
}

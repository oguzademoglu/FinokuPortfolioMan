using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Application.DTOs
{
    public record CreateAssetDto
    (
        string Name,
        int CategoryId,
        decimal Amount,
        decimal PurchasePrice,
        string Currency
    );
}

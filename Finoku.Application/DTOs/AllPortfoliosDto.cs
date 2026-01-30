using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Application.DTOs
{
    public record AllPortfoliosDto
    (
        int Id,
        string Name,
        decimal Amount,
        string Currency,
        string Username,
        string CategoryName,
        decimal CurrentPrice
    );
}

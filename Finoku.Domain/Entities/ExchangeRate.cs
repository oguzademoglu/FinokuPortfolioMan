using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Domain.Entities
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; } = string.Empty;  // from - to
        public decimal Rate { get; set; }
        public DateTime LasyUpdated { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY"; // USD, EUR, GBP 
        public decimal PurchasePrice { get; set; }
        public DateTime PurchasedAt {  get; set; }

        public int UserId { get; set; }

        //public User? User { get; set; }
    }
}

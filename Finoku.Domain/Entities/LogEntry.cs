using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Domain.Entities
{
    public class LogEntry
    {
        // Mongo id değerini string alıyormuş
        public string Id { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime Time { get; set; }
    }
}

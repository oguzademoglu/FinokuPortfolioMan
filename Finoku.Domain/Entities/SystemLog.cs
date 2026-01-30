using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Domain.Entities
{
    public class SystemLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // String olsa bile arka planda ObjectId olarak tutulmasını sağlar
        public string? Id { get; set; }
        public string TransactionType { get; set; } = string.Empty; // Add, Update, Delete
        public string UserId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string AffectedData { get; set; } = string.Empty; // Hangi tablo/entity?
        public string OldValue { get; set; } = string.Empty; // Değişim öncesi (JSON)
        public string NewValue { get; set; } = string.Empty; // Değişim sonrası (JSON)
    }
}

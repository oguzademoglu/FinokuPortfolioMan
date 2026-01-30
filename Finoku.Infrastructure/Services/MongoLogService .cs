using Finoku.Application.Interfaces;
using Finoku.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Infrastructure.Services
{
    public class MongoLogService : ILogService
    {
        private readonly IMongoCollection<SystemLog> _logs;
        public MongoLogService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _logs = database.GetCollection<SystemLog>("SystemLogs");
        }

        public async Task LogAsync(SystemLog log)
        {
            await _logs.InsertOneAsync(log);
        }
    }
}

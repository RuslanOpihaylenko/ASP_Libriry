using Books.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;
using System.Text.Json.Serialization;
using System.Text.Json;
using Books.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace Books.Infrastructure.Services
{
    public class RedisCachingService : ICachingService
    {
        private readonly IDatabase _database;
        private readonly TimeSpan _time;
        public RedisCachingService(IConnectionMultiplexer multiplexer, IOptions<CacheTimeSet> options)
        {
            _database = multiplexer.GetDatabase();
            var minutes = options.Value.CacheExpirationMinutes;
            _time = TimeSpan.FromMinutes(minutes);
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default(T?);
            }
            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);   
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? exp)
        {
            var json = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, json, exp ?? _time);
        }
    }
}

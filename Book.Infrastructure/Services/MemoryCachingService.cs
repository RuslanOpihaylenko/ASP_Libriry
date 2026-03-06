using Books.Application.Interfaces.Services;
using Books.Infrastructure.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services
{
    public class MemoryCachingService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _time;
        public MemoryCachingService(IMemoryCache memoryCache, IOptions<CacheTimeSet> options)
        {
            _memoryCache = memoryCache;
            var minutes = options.Value.CacheExpirationMinutes;
            _time = TimeSpan.FromMinutes(minutes);
        }
        public Task<T> GetAsync<T>(string key)
        {
            if(_memoryCache.TryGetValue(key, out T value))
            {
                return Task.FromResult<T?>(value);
            }
            return Task.FromResult<T?>(default);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? exp)
        {
            var optionts = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = exp ?? _time
            };
            _memoryCache.Set(key, value, optionts);
            return Task.CompletedTask;
        }
    }
}

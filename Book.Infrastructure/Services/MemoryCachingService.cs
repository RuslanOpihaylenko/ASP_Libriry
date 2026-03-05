using Books.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;
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
        public MemoryCachingService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public Task<T> GetAsync<T>(string key)
        {
            if(_memoryCache.TryGetValue(key, out var value) == true)
            {
                return Task.FromResult((T?)value);
            }
            return null;
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? exp)
        {
            throw new NotImplementedException();
        }
    }
}

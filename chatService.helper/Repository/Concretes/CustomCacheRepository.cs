using chatService.helper.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace chatService.helper.Repository.Concretes
{
    /// <summary>
    /// repository for caching operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomCacheRepository<T> : ICustomCacheRepository<T>
    {
        MemoryCacheEntryOptions _memoryCacheEntryPoints = null;
        private readonly IMemoryCache _memoryCache;

        public CustomCacheRepository()
        {
            DefineCacheOptions();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// define cache configuration
        /// </summary>
        public void DefineCacheOptions()
        {
            _memoryCacheEntryPoints = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromDays(1)
            };
        }

        public object Get(object key)
        {
            object value;

            if (_memoryCache.TryGetValue(key, out value))
            {
                return _memoryCache.Get(key);
            }

            return key;
        }

        public void Remove(object key)
        {
            _memoryCache.Remove(key);
        }

        public void Set(object key, T value)
        {
            _memoryCache.Set(key, value, _memoryCacheEntryPoints);
        }
    }
}

using chatService.helper.Service.Interfaces;
using chatService.helper.UOW.Interface;

namespace chatService.helper.Service.Concretes
{
    /// <summary>
    /// service class for caching operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomCacheService<T> : ICustomCacheService<T>
    {
        private readonly ICustomHelperUOW<T> _helper;
        public CustomCacheService(ICustomHelperUOW<T> helper)
        {
            _helper = helper;
        }
        public object GetMemoryCache(object key)
        {
            return _helper.customCacheRepositoryUOW.Get(key);
        }

        public void RemoveMemoryCache(object key)
        {
            _helper.customCacheRepositoryUOW.Remove(key);
        }

        public void SetMemoryCache(object key, T value)
        {
            _helper.customCacheRepositoryUOW.Set(key, value);
        }
    }
}

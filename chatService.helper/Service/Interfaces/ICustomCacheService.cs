namespace chatService.helper.Service.Interfaces
{
    /// <summary>
    /// service interface for caching operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomCacheService<T>
    {
        object GetMemoryCache(object key);
        void SetMemoryCache(object key, T value);
        void RemoveMemoryCache(object key);
    }
}

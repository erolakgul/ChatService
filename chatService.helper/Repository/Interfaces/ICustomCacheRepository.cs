namespace chatService.helper.Repository.Interfaces
{
    /// <summary>
    /// interface for caching operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomCacheRepository<T>
    {
        void DefineCacheOptions();
        object Get(object key);
        void Set(object key, T value);
        void Remove(object key);
    }
}

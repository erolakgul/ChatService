using chatService.helper.Repository.Interfaces;

namespace chatService.helper.UOW.Interface
{
    /// <summary>
    /// unit of work iterface items for helper service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomHelperUOW<T>
    {
        ICustomCacheRepository<T> customCacheRepositoryUOW { get; }
    }
}

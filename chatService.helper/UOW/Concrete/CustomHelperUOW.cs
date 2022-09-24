using chatService.helper.Repository.Concretes;
using chatService.helper.Repository.Interfaces;
using chatService.helper.UOW.Interface;

namespace chatService.helper.UOW.Concrete
{
    /// <summary>
    /// unit of work class items for helper service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomHelperUOW<T> : ICustomHelperUOW<T> 
    {
        private CustomCacheRepository<T> _customCacheRepository;
        public ICustomCacheRepository<T> customCacheRepositoryUOW => _customCacheRepository = _customCacheRepository ?? new CustomCacheRepository<T>();
    }
}

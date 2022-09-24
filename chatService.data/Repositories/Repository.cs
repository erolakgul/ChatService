using chatService.core.Repositories;
using chatService.helper.UOW.Concrete;

namespace chatService.data.Repositories
{
    /// <summary>
    /// main repository class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CustomHelperUOW<T> _context;
        public Repository(CustomHelperUOW<T> context)
        {
            _context = context;
        }
        /// <summary>
        /// main adding data method for all repo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T AddDto(object key, Guid guid, T entity)
        {
            if(guid != Guid.Empty ) key += guid.ToString();
            _context.customCacheRepositoryUOW.Set(key, entity); 
            return entity;
        }

        /// <summary>
        /// get data
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T GetDto(object key, Guid guid)
        {
            if (guid != Guid.Empty) key += guid.ToString();
            return (T) _context.customCacheRepositoryUOW.Get(key);
        }

        /// <summary>
        /// main removing data method for all repo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveDto(object key, Guid guid)
        {
            if (guid != Guid.Empty) key += guid.ToString();
            _context.customCacheRepositoryUOW.Remove(key);
        }
    }
}

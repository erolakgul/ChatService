using chatService.core.Repositories;

namespace chatService.data.Repositories
{
    /// <summary>
    /// main repository class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// main removing data method for all repo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveDto(object key, Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}

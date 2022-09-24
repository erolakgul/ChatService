namespace chatService.core.Repositories
{
    /// <summary>
    /// main repository interface class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// main adding data method for all repo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity AddDto(object key, Guid guid, TEntity entity);

        /// <summary>
        /// main removing data method for all repo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        void RemoveDto(object key, Guid guid);
        //List<TEntity> GetAllDto(object key);
    }
}

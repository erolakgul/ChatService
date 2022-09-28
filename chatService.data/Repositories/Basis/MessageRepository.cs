using chatService.core.DTO;
using chatService.core.Repositories.Basis;
using chatService.helper.UOW.Concrete;

namespace chatService.data.Repositories.Basis
{
    /// <summary>
    /// repository for message dto
    /// </summary>
    public class MessageRepository : Repository<List<MessageDto>>, IMessageRepository
    {
        public MessageRepository(CustomHelperUOW<List<MessageDto>> context) : base(context)
        {
        }

        // read and write will be processed on cache
        private CustomHelperUOW<List<MessageDto>> Context
        {
            get { return _context as CustomHelperUOW<List<MessageDto>>; }
        }

        //public void AddDtoList(object key, Guid guid, List<MessageDto> messageDtos)
        //{
        //    Context.customCacheRepositoryUOW.Set(key, messageDtos);
        //}

        //public List<MessageDto> GetMessages(object key, Guid guid)
        //{
        //    if (guid != Guid.Empty) key += guid.ToString();
        //    List<MessageDto> data = (List<MessageDto>)Context.customCacheRepositoryUOW.Get(key);
        //    return data;    
        //}

        /// <summary>
        /// repository specific test method 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<MessageDto> GetTestMessages(object key)
        {
            // unboxing from cache
            List<MessageDto> data = (List<MessageDto>)Context.customCacheRepositoryUOW.Get(key);
            return data;
        }
    }
}

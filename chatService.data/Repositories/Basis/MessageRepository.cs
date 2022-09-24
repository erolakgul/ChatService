using chatService.core.DTO;
using chatService.core.Repositories.Basis;
using chatService.helper.UOW.Concrete;

namespace chatService.data.Repositories.Basis
{
    /// <summary>
    /// repository for message dto
    /// </summary>
    public class MessageRepository : Repository<MessageDto>, IMessageRepository
    {
        public MessageRepository(CustomHelperUOW<MessageDto> context) : base(context)
        {
        }

        // read and write will be processed on cache
        private CustomHelperUOW<MessageDto> Context
        {
            get { return _context as CustomHelperUOW<MessageDto>; }
        }
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

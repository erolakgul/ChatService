using chatService.core.DTO;
using chatService.core.Repositories.Basis;

namespace chatService.data.Repositories.Basis
{
    /// <summary>
    /// repository for message dto
    /// </summary>
    public class MessageRepository : Repository<MessageDto>, IMessageRepository
    {
        /// <summary>
        /// repository specific test method 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<MessageDto> GetTestMessages(object key)
        {
            throw new NotImplementedException();
        }
    }
}

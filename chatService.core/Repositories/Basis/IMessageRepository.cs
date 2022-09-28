using chatService.core.DTO;

namespace chatService.core.Repositories.Basis
{
    /// <summary>
    /// interface for message dto
    /// </summary>
    public interface IMessageRepository : IRepository<List<MessageDto>>
    {
        /// <summary>
        /// repository specific method
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<MessageDto> GetTestMessages(object key);
        //List<MessageDto> GetMessages(object key, Guid guid);
        //void AddDtoList(object key, Guid guid,List<MessageDto> messageDtos);

    }
}

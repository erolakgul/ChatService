using chatService.core.DTO;

namespace chatService.core.Services.Basis
{
    /// <summary>
    /// message service
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        ///  adding method for message repository
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        //List<MessageDto> FillCollectionForMessage(object key, Guid guid, List<MessageDto> entities);
        void AddMessageList(object key, Guid guid,List<MessageDto> messageDtos);
        List<MessageDto> GetMessageList(object key, Guid guid);

        /// <summary>
        /// removing method for message repository
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        void RemoveMessage(object key, Guid guid);

    }
}

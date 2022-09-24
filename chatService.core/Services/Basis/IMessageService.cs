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
        MessageDto FillMessage(object key, Guid guid, MessageDto entity);

        /// <summary>
        /// removing method for message repository
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        void RemoveMessage(object key, Guid guid);
    }
}

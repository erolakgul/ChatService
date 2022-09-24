using chatService.core.Repositories.Basis;

namespace chatService.core.UOW
{
    /// <summary>
    /// unit of work structer interface
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// all objects
        /// </summary>
        IErrorRepository ErrorRepository { get; }
        IMessageRepository MessageRepository { get; }
    }
}

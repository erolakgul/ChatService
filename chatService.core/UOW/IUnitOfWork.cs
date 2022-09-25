using chatService.core.Repositories.Basis;
using chatService.core.Repositories.Main;

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
        ISocketRepository SocketRepository { get; } 
    }
}

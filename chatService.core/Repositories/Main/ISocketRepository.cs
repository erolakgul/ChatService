using chatService.core.DTO;
using System.Net;
using System.Net.Sockets;

namespace chatService.core.Repositories.Main
{
    public interface ISocketRepository
    {
        string GlobalSessionID { get; }
        Guid LocalSessionID { get; }
        void Start(IPEndPoint ipEndPoint);
        void TransferData(MessageDto messageDto);
        void OnConnected(IAsyncResult asyncResult);
        void OnReceived(IAsyncResult asyncResult);
        void OnShutDown(SocketShutdown socketShutdown);
    }
}

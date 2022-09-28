using chatService.core.DTO;
using System.Net;
using System.Net.Sockets;

namespace chatService.core.Services.Main
{
    public interface ISocketService
    {
        string GlobalSessionID { get; }
        Guid LocalSessionID { get; }
        void Start(IPEndPoint ipEndPoint);
        void TransferData(MessageDto messageDto);
        void OnShutDown(SocketShutdown socketShutdown);
    }
}

using chatService.core.DTO;
using System.Net;

namespace chatService.core.Repositories.Main
{
    public interface ISocketRepository
    {
        void Start(IPEndPoint ipEndPoint);
        void TransferData(MessageDto messageDto);
        void OnConnected(IAsyncResult asyncResult);
        void OnReceived(IAsyncResult asyncResult);
    }
}

using System.Net.Sockets;

namespace chatService.core.Repositories.Main
{
    public interface IClientRepository
    {
        void Start(Socket socket);
        void OnReceivedCallBack(IAsyncResult asyncResult);
        void HandleReceivedData(byte[] resizedOfBuffer);
    }
}

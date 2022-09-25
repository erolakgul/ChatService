using System.Net.Sockets;

namespace chatService.core.Services.Main
{
    public interface IClientService
    {
        void Start(Socket socket);
    }
}

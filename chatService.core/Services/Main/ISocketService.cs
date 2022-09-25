using chatService.core.DTO;
using System.Net;

namespace chatService.core.Services.Main
{
    public interface ISocketService
    {
        void Start(IPEndPoint ipEndPoint);
        void TransferData(MessageDto messageDto);
    }
}

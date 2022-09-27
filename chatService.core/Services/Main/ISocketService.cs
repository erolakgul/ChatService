using chatService.core.DTO;
using System.Net;

namespace chatService.core.Services.Main
{
    public interface ISocketService
    {
        string SessionID { get; }
        Guid SessionGUID { get; }
        void Start(IPEndPoint ipEndPoint);
        void TransferData(MessageDto messageDto);
    }
}

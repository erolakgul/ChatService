using chatService.core.DTO;
using System.Net;

namespace chatService.core.Services.Main
{
    public interface ISocketService
    {
        string GlobalSessionID { get; }
        Guid LocalSessionID { get; }
        void Start(IPEndPoint ipEndPoint);
        void TransferData(MessageDto messageDto);
    }
}

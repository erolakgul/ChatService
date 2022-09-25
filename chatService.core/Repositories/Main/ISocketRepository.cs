using chatService.core.DTO;

namespace chatService.core.Repositories.Main
{
    public interface ISocketRepository
    {
        void Start();
        void TransferData(MessageDto messageDto);
        void OnConnected(IAsyncResult asyncResult);
        void OnReceived(IAsyncResult asyncResult);
    }
}

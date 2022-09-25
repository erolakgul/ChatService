using chatService.core.DTO;

namespace chatService.core.Repositories.Main
{
    public interface IListenerRepository
    {
        void Start(int portNumber,int maxConnectionQueues);
        void OnAccepted(IAsyncResult asyncResult);
        void OnMessageReceived(MessageDto messageDto);
    }
}

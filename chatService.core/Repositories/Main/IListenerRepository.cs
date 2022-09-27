using chatService.core.DTO;

namespace chatService.core.Repositories.Main
{
    public interface IListenerRepository
    {
        string SessionID { get; }
        Guid SessionGUID { get; }
        void Start(int portNumber,int maxConnectionQueues);
        void OnAccepted(IAsyncResult asyncResult);
        void OnMessageReceived(MessageDto messageDto);
    }
}

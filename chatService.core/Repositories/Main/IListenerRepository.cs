using chatService.core.DTO;

namespace chatService.core.Repositories.Main
{
    public interface IListenerRepository
    {
        string GlobalSessionID { get; }
        Guid LocalSessionID { get; }
        void Start(int portNumber,int maxConnectionQueues);
        void OnAccepted(IAsyncResult asyncResult);
        void OnMessageReceived(MessageDto messageDto);

        Task CustomSendAsync(MessageDto messageDto); // used in server
    }
}

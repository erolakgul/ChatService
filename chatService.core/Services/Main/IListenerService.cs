using chatService.core.DTO;

namespace chatService.core.Services.Main
{
    public interface IListenerService
    {
        string GlobalSessionID { get; }
        Guid LocalSessionID { get; }
        void Start(int portNumber, int maxConnectionQueues);
        Task CustomSendFromServer(MessageDto messageDto); // used in server
    }
}

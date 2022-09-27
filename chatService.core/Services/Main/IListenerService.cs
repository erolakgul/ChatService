namespace chatService.core.Services.Main
{
    public interface IListenerService
    {
        string SessionID { get; }
        Guid SessionGUID { get; }
        void Start(int portNumber, int maxConnectionQueues);
    }
}

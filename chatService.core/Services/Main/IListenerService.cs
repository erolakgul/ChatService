namespace chatService.core.Services.Main
{
    public interface IListenerService
    {
        void Start(int portNumber, int maxConnectionQueues);
    }
}

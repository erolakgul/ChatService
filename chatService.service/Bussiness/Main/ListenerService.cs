using chatService.core.Services.Main;
using chatService.core.UOW;

namespace chatService.service.Bussiness.Main
{
    public class ListenerService : IListenerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ListenerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Start(int portNumber, int maxConnectionQueues)
        {
            _unitOfWork.ListenerRepository.Start(portNumber, maxConnectionQueues);  
        }
    }
}

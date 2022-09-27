using chatService.core.DTO;
using chatService.core.Services.Main;
using chatService.core.UOW;

namespace chatService.service.Bussiness.Main
{
    public class ListenerService : IListenerService
    {
        private readonly IUnitOfWork _unitOfWork;

        #region  creater ID
        private static string _sessionGlobalID = "";
        public string GlobalSessionID { get { return _sessionGlobalID; } }

        private static Guid _sessionLocalID = Guid.Empty;
        public Guid LocalSessionID { get { return _sessionLocalID; } } 
        #endregion


        public ListenerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Start(int portNumber, int maxConnectionQueues)
        {
            _unitOfWork.ListenerRepository.Start(portNumber, maxConnectionQueues);

            #region session and local session id
            _sessionGlobalID = _unitOfWork.ListenerRepository.GlobalSessionID;
            _sessionLocalID = _unitOfWork.ListenerRepository.LocalSessionID; 
            #endregion
        }

        public async Task CustomSendFromServer(MessageDto messageDto)
        {
            await _unitOfWork.ListenerRepository.CustomSendAsync(messageDto);
        }
    }
}

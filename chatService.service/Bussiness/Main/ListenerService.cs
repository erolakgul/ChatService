using chatService.core.Services.Main;
using chatService.core.UOW;

namespace chatService.service.Bussiness.Main
{
    public class ListenerService : IListenerService
    {
        private readonly IUnitOfWork _unitOfWork;

        private static string _sessionID = "";
        public string SessionID { get { return _sessionID; } }

        private static Guid _sessionGUID = Guid.Empty;
        public Guid SessionGUID { get { return _sessionGUID; } }
        public ListenerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Start(int portNumber, int maxConnectionQueues)
        {
            _unitOfWork.ListenerRepository.Start(portNumber, maxConnectionQueues);

            #region session and local session id
            _sessionID = _unitOfWork.ListenerRepository.SessionID;
            _sessionGUID = _unitOfWork.ListenerRepository.SessionGUID; 
            #endregion
        }
    }
}

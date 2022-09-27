using chatService.core.DTO;
using chatService.core.Services.Main;
using chatService.core.UOW;
using System.Net;

namespace chatService.service.Bussiness.Main
{
    public class SocketService : ISocketService
    {
        private readonly IUnitOfWork _unitOfWork;

        private static string _sessionID = "";
        public string SessionID { get { return _sessionID; } }

        private static Guid _sessionGUID = Guid.Empty;
        public Guid SessionGUID { get { return _sessionGUID; } }

        public SocketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Start(IPEndPoint ipEndPoint)
        {
            _unitOfWork.SocketRepository.Start(ipEndPoint);


            #region session id and local guid
            _sessionID = _unitOfWork.SocketRepository.SessionID;
            _sessionGUID = _unitOfWork.SocketRepository.SessionGUID;
            #endregion
        }

        public void TransferData(MessageDto messageDto)
        {
           _unitOfWork.SocketRepository.TransferData(messageDto);
        }
    }
}

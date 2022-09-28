using chatService.core.DTO;
using chatService.core.Services.Main;
using chatService.core.UOW;
using System.Net;
using System.Net.Sockets;

namespace chatService.service.Bussiness.Main
{
    public class SocketService : ISocketService
    {
        private readonly IUnitOfWork _unitOfWork;

        #region creater ID
        private static string _sessionGlobalID = "";
        public string GlobalSessionID { get { return _sessionGlobalID; } }

        private static Guid _sessionLocalID = Guid.Empty;
        public Guid LocalSessionID { get { return _sessionLocalID; } } 
        #endregion

        public SocketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Start(IPEndPoint ipEndPoint)
        {
            _unitOfWork.SocketRepository.Start(ipEndPoint);


            #region session id and local guid
            _sessionGlobalID = _unitOfWork.SocketRepository.GlobalSessionID;
            _sessionLocalID = _unitOfWork.SocketRepository.LocalSessionID;
            #endregion
        }

        public void TransferData(MessageDto messageDto)
        {
           _unitOfWork.SocketRepository.TransferData(messageDto);
        }

        public void OnShutDown(SocketShutdown socketShutdown)
        {
            _unitOfWork.SocketRepository.OnShutDown(socketShutdown);
        }
    }
}

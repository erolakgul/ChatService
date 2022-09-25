using chatService.core.DTO;
using chatService.core.Services.Main;
using chatService.core.UOW;
using System.Net;

namespace chatService.service.Bussiness.Main
{
    public class SocketService : ISocketService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SocketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Start(IPEndPoint ipEndPoint)
        {
            _unitOfWork.SocketRepository.Start(ipEndPoint);
        }

        public void TransferData(MessageDto messageDto)
        {
           _unitOfWork.SocketRepository.TransferData(messageDto);
        }
    }
}

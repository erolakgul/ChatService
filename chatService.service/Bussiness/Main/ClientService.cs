using chatService.core.Services.Main;
using chatService.core.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chatService.service.Bussiness.Main
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Start(Socket socket)
        {
            _unitOfWork.ClientRepository.Start(socket); 
        }
    }
}

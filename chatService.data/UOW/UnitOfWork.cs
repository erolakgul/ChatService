using chatService.core.DTO;
using chatService.core.Repositories.Basis;
using chatService.core.Repositories.Main;
using chatService.core.UOW;
using chatService.data.Repositories.Basis;
using chatService.data.Repositories.Main;
using chatService.helper.UOW.Concrete;

namespace chatService.data.UOW
{
    /// <summary>
    /// repository container
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ErrorRepository _errorRepository;
        private MessageRepository _messageRepository;
        private SocketRepository _socketRepository;
        private ListenerRepository _listenerRepository;
        private ClientRepository _clientRepository;
        
        public IErrorRepository ErrorRepository => _errorRepository = _errorRepository ?? new ErrorRepository(new CustomHelperUOW<ErrorDto>());

        public IMessageRepository MessageRepository => _messageRepository = _messageRepository ?? new MessageRepository(new CustomHelperUOW<MessageDto>());

        public ISocketRepository SocketRepository => _socketRepository = _socketRepository ?? new SocketRepository();

        public IListenerRepository ListenerRepository => _listenerRepository = _listenerRepository ?? new ListenerRepository();

        public IClientRepository ClientRepository => _clientRepository = _clientRepository ?? new ClientRepository();

    }
}

using chatService.core.DTO;
using chatService.core.Repositories.Basis;
using chatService.core.UOW;
using chatService.data.Repositories.Basis;
using chatService.helper.UOW.Concrete;
using chatService.helper.UOW.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatService.data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private ErrorRepository _errorRepository;
        private MessageRepository _messageRepository;

        public IErrorRepository ErrorRepository => _errorRepository = _errorRepository ?? new ErrorRepository(new CustomHelperUOW<ErrorDto>());

        public IMessageRepository MessageRepository => _messageRepository = _messageRepository ?? new MessageRepository(new CustomHelperUOW<MessageDto>());
    }
}

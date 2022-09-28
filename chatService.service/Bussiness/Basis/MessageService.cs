using chatService.core.DTO;
using chatService.core.Services.Basis;
using chatService.core.UOW;

namespace chatService.service.Bussiness.Basis
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddMessageList(object key, Guid guid, List<MessageDto> messageDtos)
        {
            _unitOfWork.MessageRepository.AddDto(key, guid, messageDtos);
        }

        public List<MessageDto> GetMessageList(object key, Guid guid)
        {
           return _unitOfWork.MessageRepository.GetDto(key, guid);
        }

        public void RemoveMessage(object key, Guid guid)
        {
            _unitOfWork.MessageRepository.RemoveDto(key, guid);
        }
    }
}

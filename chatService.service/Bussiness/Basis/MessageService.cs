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

        public MessageDto FillMessage(object key, Guid guid, MessageDto entity)
        {
            _unitOfWork.MessageRepository.AddDto(key, guid, entity);
            return entity;
        }

        public MessageDto GetMessage(object key, Guid guid)
        {
            return _unitOfWork.MessageRepository.GetDto(key, guid);    
        }

        public void RemoveMessage(object key, Guid guid)
        {
            _unitOfWork.MessageRepository.RemoveDto(key, guid);
        }
    }
}

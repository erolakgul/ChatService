using chatService.core.DTO;
using chatService.core.Services.Basis;
using chatService.core.UOW;

namespace chatService.service.Bussiness.Basis
{
    public class ErrorService : IErrorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ErrorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ErrorDto FillError(object key, Guid guid, ErrorDto entity)
        {
            _unitOfWork.ErrorRepository.AddDto(key, guid, entity);
            return entity;
        }
         
        public void RemoveError(object key, Guid guid)
        {
            _unitOfWork.ErrorRepository.RemoveDto(key, guid);  
        }
    }
}

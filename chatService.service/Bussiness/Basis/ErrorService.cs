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
        public void AddErrorList(object key, Guid guid, List<ErrorDto> entity)
        {
            _unitOfWork.ErrorRepository.AddDto(key, guid, entity);
        }
        public void RemoveError(object key, Guid guid)
        {
            _unitOfWork.ErrorRepository.RemoveDto(key, guid);  
        }
        public List<ErrorDto> GetErrorList(object key, Guid guid)
        {
            return _unitOfWork.ErrorRepository.GetDto(key, guid);
        }
    }
}

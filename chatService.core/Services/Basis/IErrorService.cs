using chatService.core.DTO;

namespace chatService.core.Services.Basis
{
    /// <summary>
    /// error service
    /// </summary>
    public interface IErrorService
    {
        /// <summary>
        ///  adding method for error repository
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        void AddErrorList(object key, Guid guid, List<ErrorDto> entitİES);

        /// <summary>
        /// removing method for error repository
        /// </summary>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        void RemoveError(object key, Guid guid);
        List<ErrorDto> GetErrorList(object key, Guid guid);
    }
}

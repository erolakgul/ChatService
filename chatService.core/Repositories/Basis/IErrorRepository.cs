using chatService.core.DTO;

namespace chatService.core.Repositories.Basis
{
    /// <summary>
    /// interface for error dto
    /// </summary>
    public interface IErrorRepository : IRepository<ErrorDto>
    {
        /// <summary>
        /// repository specific method 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<ErrorDto> GetTestErrors(object key);
    }
}

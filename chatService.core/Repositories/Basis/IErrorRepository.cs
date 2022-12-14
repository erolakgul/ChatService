using chatService.core.DTO;

namespace chatService.core.Repositories.Basis
{
    /// <summary>
    /// interface for error dto
    /// </summary>
    public interface IErrorRepository : IRepository<List<ErrorDto>>
    {
        /// <summary>
        /// repository specific test method 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<ErrorDto> GetTestErrors(object key);
    }
}

using chatService.core.DTO;
using chatService.core.Repositories.Basis;

namespace chatService.data.Repositories.Basis
{
    /// <summary>
    /// repository for error dto
    /// </summary>
    public class ErrorRepository : Repository<ErrorDto>, IErrorRepository
    {
        /// <summary>
        /// repository specific test method 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ErrorDto> GetTestErrors(object key)
        {
            throw new NotImplementedException();
        }
    }
}

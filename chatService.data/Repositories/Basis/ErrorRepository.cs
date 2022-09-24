using chatService.core.DTO;
using chatService.core.Repositories.Basis;
using chatService.helper.UOW.Concrete;

namespace chatService.data.Repositories.Basis
{
    /// <summary>
    /// repository for error dto
    /// </summary>
    public class ErrorRepository : Repository<ErrorDto>, IErrorRepository
    {
        public ErrorRepository(CustomHelperUOW<ErrorDto> context) : base(context)
        {
        }

        // read and write will be processed on cache
        private CustomHelperUOW<ErrorDto> Context
        {
            get { return _context as CustomHelperUOW<ErrorDto>; }
        }

        /// <summary>
        /// repository specific test method 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ErrorDto> GetTestErrors(object key)
        {
            List<ErrorDto> data = (List<ErrorDto>)Context.customCacheRepositoryUOW.Get(key);
            return data;
        }
    }
}

using DAL;
using DAL.Models;

namespace DataAccess.Repository.Abstraction
{
    public interface ILoggerRepository
    {
        Task WriteInfoLog(string message);
        Task WriteWarningLog(string message);
        Task WriteErrorLog(string message);

        Task<List<LogDto>> GetAllLogs();
    }
}

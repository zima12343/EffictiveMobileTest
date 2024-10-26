using DAL;
using DAL.Constants;
using DataAccess.Models;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.Implementation
{
    public class LoggerRepository : RepositoryBase, ILoggerRepository
    {
        public LoggerRepository(OrdersContext context) : base(context)
        {
        }

        public async Task<List<LogDto>> GetAllLogs()
        {
            return await DB.Logs.Select(x => new LogDto
            {
                Date = x.Date,
                Title = x.Title,
                TypeLog = x.TypeLog
            }).OrderBy(x => x.Date)
            .ToListAsync();
        }

        public async Task WriteErrorLog(string message)
        {
            await AddLog(message, LogTypeConstants.ERROR);
        }

        public async Task WriteInfoLog(string message)
        {
            await AddLog(message, LogTypeConstants.INFO);
        }

        public async Task WriteWarningLog(string message)
        {
            await AddLog(message, LogTypeConstants.WARNINR);
        }

        private async Task AddLog(string message, string type)
        {
            var item = new Log
            {
                Date = DateTime.Now,
                Title = message,
                TypeLog = type,
            };

            DB.Logs.Add(item);
            await DB.SaveChangesAsync();
        }
    }
}

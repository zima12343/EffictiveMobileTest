using DAL.Models.Areas;
using DataAccess.Models;
using DataAccess.Repository.Abstraction;

namespace DataAccess.Repository.Implementation
{
    public class AreaRepository : RepositoryBase, IAreaRepository
    {
        private readonly ILoggerRepository _logger;

        public AreaRepository(OrdersContext context, ILoggerRepository logger) : base(context)
        {
            this._logger = logger;
        }

        public async Task<AreaDto> Add(AddAreaDto dto)
        {
            var item = new Area
            {
                Name = dto.Name
            };

            DB.Areas.Add(item);
            await DB.SaveChangesAsync();
            await _logger.WriteInfoLog($"Save area with id {item.Id}");
            return new AreaDto
            {
                Name = item.Name,
                Id = item.Id,
            };
        }

        public async Task RemoveAll()
        {
            var items = DB.Areas;
            if (items.Any())
            {
                DB.Areas.RemoveRange(items);
                await DB.SaveChangesAsync();
                await _logger.WriteInfoLog($"Remove all areas");
            }
        }
    }
}

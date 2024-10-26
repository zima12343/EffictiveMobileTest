using DAL.Models.Orders;
using DataAccess.Models;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess.Repository.Implementation
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        private readonly ILoggerRepository _logger;

        public OrderRepository(OrdersContext context, ILoggerRepository logger) : base(context)
        {
            _logger = logger;
        }

        public async Task AddOrder(AddOrderDto dto)
        {
            var item = new Order
            {
                AreaId = dto.AreaId,
                OrderDate = dto.OrderDate,
                OrderWeight = dto.OrderWeight
            };

            DB.Orders.Add(item);
            await DB.SaveChangesAsync();
            await _logger.WriteInfoLog($"Save order with id {item.Id}");
        }

        public async Task AddRange(List<AddOrderDto> dtos)
        {
            var items = dtos.Select(dto => new Order
            {
                AreaId = dto.AreaId,
                OrderDate = dto.OrderDate,
                OrderWeight = dto.OrderWeight
            });

            DB.Orders.AddRange(items);
            await DB.SaveChangesAsync();
            await _logger.WriteInfoLog($"Save {items.Count()} orders");
        }

        public async Task RemoveAll()
        {
            var items = DB.Orders;
            if (items.Any())
            {
                DB.Orders.RemoveRange(items);
                await DB.SaveChangesAsync();
                await _logger.WriteInfoLog($"Remove all orders");
            }
        }

        public Task<List<OrderDto>> GetAllOrders()
        {
            return DB.Orders
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderWeight = x.OrderWeight,
                    AreaId = x.AreaId,
                    AreaName = x.Area.Name
                })
                .OrderBy(x => x.OrderDate)
                .ToListAsync();
        }

        public Task<List<OrderDto>> GetFiltredOrders(Guid areaId, DateTime dateTimeOrder)
        {
            return DB.Orders
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderWeight = x.OrderWeight,
                    AreaId = x.AreaId,
                    AreaName = x.Area.Name
                })
                .Where(x => x.AreaId == areaId && x.OrderDate >= dateTimeOrder && x.OrderDate <= dateTimeOrder.AddMinutes(30))
                .OrderBy(x => x.OrderDate)
                .ToListAsync();
        }
    }
}

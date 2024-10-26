using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class OrdersContext: DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> option):base(option)
        {
            
        }

        public DbSet<Area> Areas{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Log> Logs{ get; set; }

    }
}

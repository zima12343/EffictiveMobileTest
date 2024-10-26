using DAL.Models.Orders;

namespace DataAccess.Repository.Abstraction
{
    public interface IOrderRepository
    {
        Task AddOrder(AddOrderDto dto);
        Task AddRange(List<AddOrderDto> dtos);
        Task RemoveAll();
        Task<List<OrderDto>> GetAllOrders();
        Task<List<OrderDto>> GetFiltredOrders(Guid areaId, DateTime dateTimeOrder);
    }
}

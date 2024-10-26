using DataAccess.Repository.Abstraction;

namespace Services.DataGenerator
{
    public class DataDenerator : IDataGenerator
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IOrderRepository _orderRepository;

        public DataDenerator(IAreaRepository areaRepository, IOrderRepository orderRepository)
        {
            _areaRepository = areaRepository;
            _orderRepository = orderRepository;
        }

        public async Task GenerateAreaWithOrders(int areaAmount, int amountInArea = 25)
        {
            await _areaRepository.RemoveAll();
            await _orderRepository.RemoveAll();
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < areaAmount; i++)
            {
                var area = await _areaRepository.Add(new DAL.Models.Areas.AddAreaDto { Name = $"Area {i + 1}" });
                var orders = new List<DAL.Models.Orders.AddOrderDto>(amountInArea);
                for (int j = 0; j < amountInArea; j++)
                {
                    orders.Add(new DAL.Models.Orders.AddOrderDto
                    {
                        AreaId = area.Id,
                        OrderDate = DateTime.Now.AddSeconds(random.Next(84000, 84000 * 5)),
                        OrderWeight = Convert.ToDecimal(random.Next(10000, 100000) / 100.0)
                    });
                }
                await _orderRepository.AddRange(orders);
            }
        }
    }
}

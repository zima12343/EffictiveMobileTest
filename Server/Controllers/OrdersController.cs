using DataAccess.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly ILoggerRepository _logger;

        public OrdersController(IOrderRepository repository, ILoggerRepository logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("AllOrders")]
        public async Task<ActionResult> GetAllOrders()
        {
            var items = await _repository.GetAllOrders();
            if (!items.Any())
            {
                return NoContent();
            }
            var resultStr = "Date\t\t\tOrder id\t\t\t\tArea Name \t Area Id\n\n";

            items.ForEach(x => resultStr += $"{x.OrderDate.ToString("dd.MM.yyyy hh:mm:ss")}\t{x.Id}\t{x.AreaName}\t{x.AreaId}\n");

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "AllOrders.txt",
                Inline = false
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());
            Response.Headers.Append("X-Content-Type-Options", "nosniff");

            await _logger.WriteInfoLog("Getting all orders");
            return File(Encoding.UTF8.GetBytes(resultStr), "application/text");
        }


        [HttpGet("FilterOrders")]
        public async Task<ActionResult> GetAllOrders(Guid areaId, DateTime dateTimeOrder)
        {
            //"2024-10-26T21:28:41.4910801+03:00"
            var items = await _repository.GetFiltredOrders(areaId, dateTimeOrder);
            if (!items.Any())
            {
                return NoContent();
            }
            var resultStr = "Date\t\t\tOrder id\t\t\t\tArea Name \t Area Id\n\n";

            items.ForEach(x => resultStr += $"{x.OrderDate.ToString("dd.MM.yyyy hh:mm:ss")}\t{x.Id}\t{x.AreaName}\t{x.AreaId}\n");

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "FiltredOrders.txt",
                Inline = false
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());
            Response.Headers.Append("X-Content-Type-Options", "nosniff");

            await _logger.WriteInfoLog($"Getting filtred orders by areaId = {areaId} datetime = {dateTimeOrder}");
            return File(Encoding.UTF8.GetBytes(resultStr), "application/text");
        }
    }
}

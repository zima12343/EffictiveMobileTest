using DAL;
using DataAccess.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoggerController: ControllerBase
    {
        private readonly ILoggerRepository _logger;

        public LoggerController(ILoggerRepository logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var items = await _logger.GetAllLogs();
            if (!items.Any())
            {
                return NoContent();
            }
            var resultStr = "";

            items.ForEach(x => resultStr += $"{x.Date.ToString("dd.MM.yyyy hh:mm:ss")}\t{x.TypeLog}\t{x.Title}\n");

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "logs.txt",
                Inline = false 
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(Encoding.UTF8.GetBytes(resultStr), "application/text");
        }
    }
}

using DataAccess.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Services.DataGenerator;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataGeneratorController
    {
        private readonly IDataGenerator _dataGenerator;
        private readonly ILoggerRepository logger;

        public DataGeneratorController(IDataGenerator dataGenerator, ILoggerRepository logger)
        {
            _dataGenerator = dataGenerator;
            this.logger = logger;
        }

        [HttpPost]
        public void GenerateData(int areasAmount, int amountInArea = 25)
        {
            logger.WriteInfoLog("Start generate data");
            _dataGenerator.GenerateAreaWithOrders(areasAmount, amountInArea);
            logger.WriteInfoLog("End generate data");
        }
    }
}
